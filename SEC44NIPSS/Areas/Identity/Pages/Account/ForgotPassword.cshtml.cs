using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using SEC44NIPSS.Data.Model;


namespace SEC44NIPSS.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class ForgotPasswordModel : PageModel
    {
        private readonly SEC44NIPSS.Data.NIPSSDbContext _context;
        private readonly IHostingEnvironment _hostingEnv;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        public ForgotPasswordModel(SEC44NIPSS.Data.NIPSSDbContext context, IHostingEnvironment hostingEnv, SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _hostingEnv = hostingEnv;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public Message i { get; set; }


        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }
        }
        public void OnGet()
        {

        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var userx = await _userManager.FindByEmailAsync(Input.Email);
                if (userx == null)
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    TempData["er"] = "Invalid Email Address";
                    return Page();
                }
                var user = await _context.Profiles.Include(x => x.User).FirstOrDefaultAsync(x => x.UserId == userx.Id);

                // For more information on how to enable account confirmation and password reset please 
                // visit https://go.microsoft.com/fwlink/?LinkID=532713
                var code = await _userManager.GeneratePasswordResetTokenAsync(userx);
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                var callbackUrl = Url.Page(
                    "/Account/ResetPassword",
                    pageHandler: null,
                    values: new { area = "Identity", code },
                    protocol: Request.Scheme);


                StreamReader sr = new StreamReader(System.IO.Path.Combine(_hostingEnv.WebRootPath, "emailsec.html"));
                MailMessage mail = new MailMessage();
                string mi = $"Please reset your password by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.";


                string mailmsg = sr.ReadToEnd();
                mailmsg = mailmsg.Replace("{NAME}", user.Title + " " + user.FullName);
                mailmsg = mailmsg.Replace("{TITLE}", "Reset Password");
                mailmsg = mailmsg.Replace("{BODY}", mi);
                mail.Body = mailmsg;
                sr.Close();

                Message ms = new Message();
                ms.Recipient = user.User.Email;
                ms.Title = "Reset Password";
                ms.Mail = mailmsg;
                ms.Retries = 0; ms.NotificationStatus = NotificationStatus.NotSent; ms.NotificationType = NotificationType.Email;
                ms.NotificationStatus = NotificationStatus.NotSent;
                _context.Messages.Add(ms);
                await _context.SaveChangesAsync();


                //for (int i = 0; i < 500; i++)
                //{
                //    var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
                //    var stringChars = new char[8];
                //    var random = new Random();

                //    for (int xi = 0; xi < stringChars.Length; xi++)
                //    {
                //        stringChars[xi] = chars[random.Next(chars.Length)];
                //    }

                //    var finalString = new String(stringChars);
                //    Message xms = new Message();
                //    xms.Recipient = finalString+"@gmail.com";
                //    xms.Title = "Reset Password";
                //    xms.Mail = mailmsg;
                //    xms.Retries = 0; xms.NotificationStatus = NotificationStatus.NotSent; xms.NotificationType = NotificationType.Email;
                //    xms.NotificationStatus = NotificationStatus.NotSent;
                //    _context.Messages.Add(xms);

                //}
                //await _context.SaveChangesAsync();
                return RedirectToPage("./ForgotPasswordConfirmation");
            }

            return Page();
        }


    }
}
