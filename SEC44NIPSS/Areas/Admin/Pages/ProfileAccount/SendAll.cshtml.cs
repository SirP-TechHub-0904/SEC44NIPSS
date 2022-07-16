using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SEC44NIPSS.Data.Model;

namespace SEC44NIPSS.Areas.Admin.Pages.ProfileAccount
{
    public class SendAllModel : PageModel
    {
        private readonly SEC44NIPSS.Data.NIPSSDbContext _context;
        private readonly IHostingEnvironment _hostingEnv;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        public SendAllModel(SEC44NIPSS.Data.NIPSSDbContext context, IHostingEnvironment hostingEnv, SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _hostingEnv = hostingEnv;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public Message i { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            IQueryable<Profile> profilex = from s in _context.Profiles.Include(x => x.User).Where(x => x.User.Email != "jinmcever@gmail.com").OrderByDescending(x => x.FullName)
                                           select s;
            var Profile = await profilex.Where(x => x.AccountRole == "Participant" && x.Sent ==false).ToListAsync();
            foreach (var user in Profile)
            {

                //var user = await _context.Profiles.Include(x => x.User).FirstOrDefaultAsync(x => x.UserId == x.UserId);
                var callbackUrl = Url.Page(
                            "/Account/Firstsignin",
                            pageHandler: null,
                            values: new { area = "Identity", userId = user.User.Email, code = user.User.SecurityStamp },
                            protocol: Request.Scheme);
                StreamReader sr = new StreamReader(System.IO.Path.Combine(_hostingEnv.WebRootPath, "emailsec.html"));
                MailMessage mail = new MailMessage();
                string mi = $"Kindly Login via the link <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>click here</a>.<br>" +
                    $"<h5>OR Visit <a href='www.sec44nipss.com'>www.sec44nipss.com</a> and click on VLE on the menu.</h6><br>" +
                    $"<p>Email: " + user.User.Email + "</p>" +
                    $"<p>Password: " + user.PXI + "</p>";

                string mailmsg = sr.ReadToEnd();
                mailmsg = mailmsg.Replace("{NAME}", user.Title + " " + user.FullName);
                mailmsg = mailmsg.Replace("{TITLE}", "Account Setup");
                mailmsg = mailmsg.Replace("{BODY}", mi);
                mail.Body = mailmsg;
                sr.Close();

                Message ms = new Message();
                ms.Recipient = user.User.Email;
                ms.Title = "Account Setup";
                ms.Mail = mailmsg;
                ms.Retries = 0; ms.NotificationStatus = NotificationStatus.NotSent; ms.NotificationType = NotificationType.Email;
                _context.Messages.Add(ms);
                user.Sent = true;
                _context.Attach(user).State = EntityState.Modified;
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
            }
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

                return false;
            }
        }

    }
}
