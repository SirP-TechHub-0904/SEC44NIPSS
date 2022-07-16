using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Hosting;
using System.Net.Mail;
using System.Net;
using Microsoft.EntityFrameworkCore;
using System.IO;
using SEC44NIPSS.Data.Model;

namespace SEC44NIPSS.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RequestEmailModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ILogger<LoginModel> _logger;
        private readonly IHostingEnvironment _hostingEnv;
        private readonly SEC44NIPSS.Data.NIPSSDbContext _context;


        public RequestEmailModel(SignInManager<IdentityUser> signInManager,
            ILogger<LoginModel> logger,
            UserManager<IdentityUser> userManager, IHostingEnvironment hostingEnv, Data.NIPSSDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _hostingEnv = hostingEnv;
            _context = context;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public string ReturnUrl { get; set; }

        [BindProperty]
        public string Fullname { get; set; }

        [BindProperty]
        public string Phone { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }

        }

        public async Task OnGetAsync()
        {
           
        }
        public Message i { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            //  returnUrl = returnUrl ?? Url.Content("~/");

            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(Input.Email);
                if (user != null)
                {
                    var profile = await _context.Profiles.FirstOrDefaultAsync(x => x.UserId == user.Id);
                    var callbackUrl = Url.Page(
                       "/Account/Firstsignin",
                       pageHandler: null,
                       values: new { area = "Identity", userId = user.Email, code = user.SecurityStamp },
                       protocol: Request.Scheme);
                    StreamReader sr = new StreamReader(System.IO.Path.Combine(_hostingEnv.WebRootPath, "emailsec.html"));
                    MailMessage mail = new MailMessage();
                    string mi = $"Kindly Login via the link <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>click here</a>.<br>" +
                        $"<h5>OR Visit <a href='www.sec44nipss.com'>www.sec44nipss.com</a> and click on VLE on the menu.</h6><br>" +
                        $"<p>Email: " + profile.User.Email + "</p>" +
                        $"<p>Password: " + profile.PXI + "</p>";

                    string mailmsg = sr.ReadToEnd();
                    mailmsg = mailmsg.Replace("{NAME}", profile.Title + " " + profile.FullName);
                    mailmsg = mailmsg.Replace("{TITLE}", "Account Setup");
                    mailmsg = mailmsg.Replace("{BODY}", mi);
                    mail.Body = mailmsg;
                    sr.Close();

                    Message ms = new Message();
                    ms.Recipient = profile.User.Email;
                    ms.Title = "Account Setup";
                    ms.Mail = mailmsg;
                    ms.Retries = 0; ms.NotificationStatus = NotificationStatus.NotSent; ms.NotificationType = NotificationType.Email;
                    ms.NotificationStatus = NotificationStatus.NotSent;
                    _context.Messages.Add(ms);
                    await _context.SaveChangesAsync();



                 
                }
                else
                {
                    TempData["error"] = "Error";
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }


        public async Task<IActionResult> OnPostSendForm()
        {
            //  returnUrl = returnUrl ?? Url.Content("~/");

            if (ModelState.IsValid)
            {
             
                    StreamReader sr = new StreamReader(System.IO.Path.Combine(_hostingEnv.WebRootPath, "emailsec.html"));
                    MailMessage mail = new MailMessage();
                    string mi =
                        $"<p>Email: " + Input.Email + "</p>" +
                        $"<p>phone: " + Phone + "</p>"+
                        $"<p>Name: " + Fullname + "</p>";
                        

                    string mailmsg = sr.ReadToEnd();
                    mailmsg = mailmsg.Replace("{NAME}", Fullname);
                    mailmsg = mailmsg.Replace("{TITLE}", "Check Account");
                    mailmsg = mailmsg.Replace("{BODY}", mi);
                    mail.Body = mailmsg;
                    sr.Close();

                    Message ms = new Message();
                    ms.Recipient = "onwukaemeka41@gmail.com";
                    ms.Title = "Check Account";
                    ms.Mail = mailmsg;
                    ms.Retries = 0; ms.NotificationStatus = NotificationStatus.NotSent; ms.NotificationType = NotificationType.Email;
                ms.NotificationStatus = NotificationStatus.NotSent;
                _context.Messages.Add(ms);
                    await _context.SaveChangesAsync();

                TempData["request"] = "dfgf";
                
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
