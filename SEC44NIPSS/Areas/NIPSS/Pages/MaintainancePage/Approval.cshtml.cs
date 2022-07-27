using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SEC44NIPSS.Data;
using SEC44NIPSS.Data.Model;

namespace SEC44NIPSS.Areas.NIPSS.Pages.MaintainancePage
{
    [Authorize(Roles = "Admin")]

    public class ApprovalModel : PageModel
    {
        private readonly SEC44NIPSS.Data.NIPSSDbContext _context;
        private readonly IHostingEnvironment _hostingEnv;

        public ApprovalModel(SEC44NIPSS.Data.NIPSSDbContext context, IHostingEnvironment hostingEnv)
        {
            _context = context;
            _hostingEnv = hostingEnv;
        }

        public Ticket Ticket { get; set; }


        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Ticket = await _context.Tickets
                .Include(t => t.ApprovedBy)
                .Include(t => t.ForwardedTo)
                .Include(t => t.JobCompletionCertifiedBy)
                .Include(t => t.ReceivedAndPassTo)
                .Include(t => t.User).FirstOrDefaultAsync(m => m.Id == id);

            if (Ticket == null)
            {
                return NotFound();
            }

            var acc = await _context.Profiles.Where(x => x.AccountRole == "Staff").OrderByDescending(x=>x.FullName).ToListAsync();

            ViewData["ProfileId"] = new SelectList(acc, "Id", "FullName");
            return Page();
        }

        [BindProperty]
        public long TicketId { get; set; }

        [BindProperty]
        public long ApprovedId { get; set; }

        [BindProperty]
        public string ApprovedNote { get; set; }
        public async Task<IActionResult> OnPostApproval()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var tik = await _context.Tickets.FirstOrDefaultAsync(x => x.Id == TicketId);
            tik.ApprovedById = ApprovedId;
            tik.NoteApprovedBy = ApprovedNote;
            tik.ApprovedByTime = DateTime.UtcNow.AddHours(1);
            _context.Attach(tik).State = EntityState.Modified;

            
                await _context.SaveChangesAsync();


            var Profile = await _context.Profiles.Include(x => x.User).FirstOrDefaultAsync(x => x.Id == ApprovedId);
           
            TicketStage tstage = new TicketStage();
            tstage.Title = "Approved by " + Profile.Title + "" + Profile.FullName;
            tstage.Date = DateTime.UtcNow.AddHours(1);
            tstage.TicketId = TicketId;
            _context.TicketStages.Add(tstage);
            await _context.SaveChangesAsync();
            var callbackUrl = Url.Page(
                      "/MaintainancePage/Details",
                      pageHandler: null,
                      values: new { area = "NIPSS", id = TicketId },
                      protocol: Request.Scheme);
            StreamReader sr = new StreamReader(System.IO.Path.Combine(_hostingEnv.WebRootPath, "emailsec.html"));
            MailMessage mail = new MailMessage();
            string mi = $"Request with Number (" + tik.TicketNumber + ") on Maintenance was approved by <br>================================<br>Name: " + Profile.Title + " " + Profile.FullName + "<br>===================<br>" +
                 
                "<br>";


            string mailmsg = sr.ReadToEnd();
            mailmsg = mailmsg.Replace("{NAME}", Profile.FullName);
            mailmsg = mailmsg.Replace("{TITLE}", "");
            mailmsg = mailmsg.Replace("{BODY}", mi);
            mail.Body = mailmsg;
            sr.Close();

            Message ms = new Message();
            ms.Recipient = Profile.User.Email;
            ms.Title = "Ticket {" + tik.TicketNumber + "} Approved by you";
            ms.Mail = mailmsg;
            ms.Retries = 0; ms.NotificationStatus = NotificationStatus.NotSent; ms.NotificationType = NotificationType.Email;
            _context.Messages.Add(ms);
            await _context.SaveChangesAsync();

            //sms
            Message sms = new Message();
            sms.Recipient = Profile.PhoneNumber;
            sms.Title = "Ticket {" + tik.TicketNumber + "} Approved by you";
            sms.Mail = "Ticket {" + tik.TicketNumber + "} Approved by you. Kindly check your dashboard";
            sms.Retries = 0; sms.NotificationStatus = NotificationStatus.NotSent; sms.NotificationType = NotificationType.SMS;
            _context.Messages.Add(sms);
            await _context.SaveChangesAsync();

            var ticketsupervisor = await _context.TicketSupervisor.ToListAsync();
            try
            {

                foreach (var mm in ticketsupervisor)
                {
                    long emx = 0;
                    long ymx = 0;
                    if (mm.SendEmail == true)
                    {
                        StreamReader xsr = new StreamReader(System.IO.Path.Combine(_hostingEnv.WebRootPath, "emailsec.html"));
                        MailMessage xmail = new MailMessage();
                        string xm = "Maintenance Departmnt: "+ Profile.Title + " " + Profile.FullName + " has Approved Ticket <br>================================<br>" +
                            $"<a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here to comfirm</a>.<br><br>================================<br>" + mi;

                        string xmailmsg = xsr.ReadToEnd();
                        xmailmsg = xmailmsg.Replace("{NAME}", mm.Name);
                        xmailmsg = xmailmsg.Replace("{TITLE}", "");
                        xmailmsg = xmailmsg.Replace("{BODY}", xm);
                        xmail.Body = xmailmsg;
                        xsr.Close();

                        Message mms = new Message();
                        mms.Recipient = mm.Email;
                        mms.Title = "Ticket {" + tik.TicketNumber + "} was Approved by " + Profile.Title +" "+Profile.FullName;
                        mms.Mail = xmailmsg;
                        mms.Retries = 0; mms.NotificationStatus = NotificationStatus.NotSent; mms.NotificationType = NotificationType.Email;
                        _context.Messages.Add(mms);
                        await _context.SaveChangesAsync();
                        emx = mms.Id;
                    }
                    //sms
                    if (mm.SendPhone == true)
                    {
                        Message msms = new Message();
                        msms.Recipient = mm.Phone;
                        msms.Title = "Ticket {" + tik.TicketNumber + "} was Approved by " + Profile.Title + " " + Profile.FullName;
                        msms.Mail = "Ticket {" + tik.TicketNumber + "} was Approved by " + Profile.Title + " " + Profile.FullName;
                        msms.Retries = 0; msms.NotificationStatus = NotificationStatus.NotSent; msms.NotificationType = NotificationType.SMS;
                        _context.Messages.Add(msms);
                        await _context.SaveChangesAsync();
                        ymx = msms.Id;
                    }


                }
            }
            catch (Exception c)
            {

            }


            TempData["status"] = "Added Successfully";
            return RedirectToPage("./Details", new { id = TicketId });
        }

    }
}
