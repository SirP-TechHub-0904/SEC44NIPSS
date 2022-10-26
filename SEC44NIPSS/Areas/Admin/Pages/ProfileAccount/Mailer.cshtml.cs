using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SEC44NIPSS.Data.Model;

namespace SEC44NIPSS.Areas.Admin.Pages.ProfileAccount
{
    [Authorize(Roles = "mSuperAdmin,Admin")]
    public class MailerModel : PageModel
    {
        private readonly SEC44NIPSS.Data.NIPSSDbContext _context;
        private readonly IHostingEnvironment _hostingEnv;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        public MailerModel(SEC44NIPSS.Data.NIPSSDbContext context, IHostingEnvironment hostingEnv, SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _hostingEnv = hostingEnv;
            _signInManager = signInManager;
            _userManager = userManager;
        }
        [BindProperty]
        public Message ms { get; set; }

        [BindProperty]
        public string Messagex { get; set; }

        [BindProperty]
        public string Name { get; set; }

        
        public async Task<IActionResult> OnGetAsync()
        {
            //ViewData["EmailId"] = new SelectList(_context.EmailSettings, "Id", "SenderEmail");

            return Page();
        }
        public Message i { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {


            StreamReader sr = new StreamReader(System.IO.Path.Combine(_hostingEnv.WebRootPath, "emailsec.html"));
            MailMessage mail = new MailMessage();

            string mailmsg = sr.ReadToEnd();
            mailmsg = mailmsg.Replace("{NAME}", Name );
            mailmsg = mailmsg.Replace("{TITLE}", ms.Title);
            mailmsg = mailmsg.Replace("{BODY}", Messagex);
            mail.Body = mailmsg;
            sr.Close();


            ms.Mail = mailmsg;
            ms.Retries = 0; ms.NotificationStatus = NotificationStatus.NotSent; ms.NotificationType = NotificationType.Email;
            _context.Messages.Add(ms);
            await _context.SaveChangesAsync();

      

            i = await _context.Messages.FirstOrDefaultAsync(m => m.Id == ms.Id);

            if (i == null)
            {
                return NotFound();
            }
            if (i.NotificationType != NotificationType.SMS)
            {
                //
                bool result = await SendEmail(i.Recipient, i.Mail, i.Title);
                if (result == true)
                {
                    i.NotificationStatus = NotificationStatus.Sent;
                    TempData["success"] = "Sent";
                }
                else
                {
                    i.NotificationStatus = NotificationStatus.NotSent;
                    i.Retries = i.Retries + 1;
                    TempData["error"] = "failed";
                }
            }

            try
            {

                var iod = await _context.Messages.AsNoTracking().FirstOrDefaultAsync(x => x.Id == i.Id);
                iod.NotificationStatus = i.NotificationStatus;
                iod.Retries = i.Retries;
                //_context.Entry(iod).State = EntityState.Detached;
                _context.Attach(iod).State = EntityState.Modified;


            }

            catch (Exception webex)
            {

            }



            await _context.SaveChangesAsync();

            return Page();
        }



        public async Task<bool> SendEmail(string recipient, string message, string title)
        {
            try
            {


                //create the mail message 
                MailMessage mail = new MailMessage();


                mail.Body = message;
                //set the addresses 
                mail.From = new MailAddress("admin@sec44nipss.com", "SEC44 NIPSS"); //IMPORTANT: This must be same as your smtp authentication address.
                mail.To.Add(recipient);

                //set the content 
                mail.Subject = title.Replace("\r\n", "");

                mail.IsBodyHtml = true;
                //send the message 
                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);

                //IMPORANT:  Your smtp login email MUST be same as your FROM address. 
                NetworkCredential Credentials = new NetworkCredential("admin@sec44nipss.com", "yvjholmmateuysye");
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = Credentials;
                smtp.Timeout = 20000;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.EnableSsl = true;
                smtp.Send(mail);
                ///

                
                return true;
            }
            catch (Exception ex)
            {
                TempData["lo"] = ex.ToString();
                return false;
            }
        }

    }
}
