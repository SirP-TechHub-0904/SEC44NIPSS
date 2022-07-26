using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Mail;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using System.Web;
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

    public class StaffAssignModel : PageModel
    {
        private readonly SEC44NIPSS.Data.NIPSSDbContext _context;
        private readonly IHostingEnvironment _hostingEnv;

        public StaffAssignModel(SEC44NIPSS.Data.NIPSSDbContext context, IHostingEnvironment hostingEnv)
        {
            _context = context;
            _hostingEnv = hostingEnv;
        }

        public IList<TicketStaff> TicketStaffs { get; set; }
        [BindProperty]
        public TicketStaff TicketStaff { get; set; }


        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            TicketStaffs = await _context.TicketStaff.Where(x => x.TicketId == id).ToListAsync();
            TicketId = id ?? 0;
            var acc = await _context.Profiles.Where(x => x.AccountRole == "Staff").OrderByDescending(x => x.FullName).ToListAsync();

            ViewData["ProfileId"] = new SelectList(acc, "Id", "FullName");
            return Page();
        }

        [BindProperty]
        public long TicketId { get; set; }

        public async Task<IActionResult> OnPostAssignStaff()
        {

            TicketStaff.TicketId = TicketId;
            TicketStaff.TicketStaffStatus = TicketStaffStatus.Active;
            TicketStaff.Date = DateTime.UtcNow.AddHours(1);
            _context.TicketStaff.Add(TicketStaff);
            await _context.SaveChangesAsync();
            var Profile = await _context.Profiles.Include(x=>x.User).FirstOrDefaultAsync(x => x.Id == TicketStaff.ProfileId);
            var tik = await _context.Tickets.FirstOrDefaultAsync(x => x.Id == TicketStaff.TicketId);
            StreamReader sr = new StreamReader(System.IO.Path.Combine(_hostingEnv.WebRootPath, "emailsec.html"));
            MailMessage mail = new MailMessage();
            string mi = $"Staff Assign to Ticket (" + tik.TicketNumber + ")<br>================================<br>" + Profile.Title + " " + Profile.FullName + " has been Assigned work to do <br>===================<br> " + TicketStaff.WorkCarriedOut + "<br><br>";
              

            string mailmsg = sr.ReadToEnd();
            mailmsg = mailmsg.Replace("{NAME}", Profile.FullName);
            mailmsg = mailmsg.Replace("{TITLE}", "");
            mailmsg = mailmsg.Replace("{BODY}", mi);
            mail.Body = mailmsg;
            sr.Close();

            Message ms = new Message();
            ms.Recipient = Profile.User.Email;
            ms.Title = "Ticket {" + tik.TicketNumber + "} has been Updated";
            ms.Mail = mailmsg;
            ms.Retries = 0; ms.NotificationStatus = NotificationStatus.NotSent; ms.NotificationType = NotificationType.Email;
            _context.Messages.Add(ms);
            await _context.SaveChangesAsync();

            //sms
            Message sms = new Message();
            sms.Recipient = Profile.PhoneNumber;
            sms.Title = "Ticket {" + tik.TicketNumber + "} has been Updated";
            sms.Mail = "You were assigned to work on Ticket {" + tik.TicketNumber + "}. Kindly check your dashboard";
            sms.Retries = 0; sms.NotificationStatus = NotificationStatus.NotSent; sms.NotificationType = NotificationType.SMS;
            _context.Messages.Add(sms);
            await _context.SaveChangesAsync();

            var callbackUrl = Url.Page(
                         "/MaintainancePage/Details",
                         pageHandler: null,
                         values: new { area = "NIPSS", id = TicketId },
                         protocol: Request.Scheme);
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
                        string xm = "Staff Assigned to work on Ticket <br>================================<br>" +
                            $"<a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here to comfirm</a>.<br><br>================================<br>" + mi;

                        string xmailmsg = xsr.ReadToEnd();
                        xmailmsg = xmailmsg.Replace("{NAME}", mm.Name);
                        xmailmsg = xmailmsg.Replace("{TITLE}", "");
                        xmailmsg = xmailmsg.Replace("{BODY}", xm);
                        xmail.Body = xmailmsg;
                        xsr.Close();

                        Message mms = new Message();
                        mms.Recipient = mm.Email;
                        mms.Title = "Ticket {" + tik.TicketNumber + "} has been Assign a Staff";
                        mms.Mail =  xmailmsg;
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
                        msms.Title = "Ticket {" + tik.TicketNumber + "} has been Assign a Staff";
                        msms.Mail = "Ticket {" + tik.TicketNumber + "} has been Assign a Staff";
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
            return RedirectToPage("./StaffAssign", new { id = TicketId });
        }

        [BindProperty]
        public long? TicketStaffId { get; set; }

        public async Task<IActionResult> OnPostRemoved()
        {
            var mx = await _context.TicketStaff.FirstOrDefaultAsync(x => x.Id == TicketStaffId);
            mx.TicketStaffStatus = TicketStaffStatus.Remove;
            _context.Attach(mx).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            TempData["status"] = "Added Successfully";
            return RedirectToPage("./StaffAssign", new { id = TicketId });
        }

        public async Task<IActionResult> OnPostActive()
        {
            var mx = await _context.TicketStaff.FirstOrDefaultAsync(x => x.Id == TicketStaffId);
            mx.TicketStaffStatus = TicketStaffStatus.Remove;
            _context.Attach(mx).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            TempData["status"] = "Added Successfully";
            return RedirectToPage("./StaffAssign", new { id = TicketId });
        }

        public async Task<IActionResult> OnPostChange()
        {
            var mx = await _context.TicketStaff.FirstOrDefaultAsync(x => x.Id == TicketStaffId);
            mx.TicketStaffStatus = TicketStaffStatus.Changed;
            _context.Attach(mx).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            TempData["status"] = "Added Successfully";
            return RedirectToPage("./StaffAssign", new { id = TicketId });
        }


      
    }
}
