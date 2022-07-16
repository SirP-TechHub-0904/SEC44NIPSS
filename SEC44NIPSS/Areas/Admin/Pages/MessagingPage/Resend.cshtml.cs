using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SEC44NIPSS.Data.Model;

namespace SEC44NIPSS.Areas.Admin.Pages.MessagingPage
{
    public class ResendModel : PageModel
    {
        private readonly SEC44NIPSS.Data.NIPSSDbContext _context;

        public ResendModel(SEC44NIPSS.Data.NIPSSDbContext context)
        {
            _context = context;
        }

        public Message i { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            i = await _context.Messages.FirstOrDefaultAsync(m => m.Id == id);

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
                    TempData["sendt"] = "Sent";
                }
                else
                {
                    i.NotificationStatus = NotificationStatus.NotSent;
                    i.Retries = i.Retries + 1;
                    TempData["sendt"] = "failed";
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
                mail.From = new MailAddress("noreply@sec44nipss.com", "SEC44 NIPSS"); //IMPORTANT: This must be same as your smtp authentication address.
                mail.To.Add(recipient);

                //set the content 
                mail.Subject = title.Replace("\r\n", "");

                mail.IsBodyHtml = true;
                //send the message 
                SmtpClient smtp = new SmtpClient("mail.sec44nipss.com");

                //IMPORANT:  Your smtp login email MUST be same as your FROM address. 
                NetworkCredential Credentials = new NetworkCredential("noreply@sec44nipss.com", "Admin@123");
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = Credentials;
                smtp.Port = 25;    //alternative port number is 8889
                smtp.EnableSsl = false;
                smtp.Send(mail);
                return true;
            }
            catch (Exception ex)
            {
                TempData["err"] = ex.Message.ToString();
                return false;
            }
        }

    }
}
