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
    public class SmsSenderModel : PageModel
    {
        private readonly SEC44NIPSS.Data.NIPSSDbContext _context;
        private readonly IHostingEnvironment _hostingEnv;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        public SmsSenderModel(SEC44NIPSS.Data.NIPSSDbContext context, IHostingEnvironment hostingEnv, SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
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

        [BindProperty]
        public string SendTo { get; set; }

       
       
        public async Task<IActionResult> OnPostAsync()
        {
            int countitem = 0;

           
            if(SendTo == "Participant")
            {
                var parti =  _context.Participants.Include(x=>x.Profile).ThenInclude(x=>x.User).Where(x => x.IsTrue == true).AsQueryable();
                int cs = parti.Count();
                foreach(var x in parti)
                {
                    if (x.Profile.User != null)
                    {
                        if (!String.IsNullOrEmpty(x.Profile.PhoneNumber))
                        {
                            Message xm = new Message();
                            xm.Title = ms.Title;
                            xm.Mail = Messagex;
                            xm.Recipient = x.Profile.PhoneNumber;
                            xm.Retries = 0; xm.NotificationStatus = NotificationStatus.NotSent; xm.NotificationType = NotificationType.SMS;
                            _context.Messages.Add(xm);

                            countitem++;
                        }
                    }
                }
                await _context.SaveChangesAsync();
            }else if (SendTo == "All")
            {
                var parti = _context.Profiles.Include(x => x.User).AsQueryable();

                int cs = parti.Count();

                foreach (var x in parti)
                {
                    if (x.User != null)
                    {
                        if (!String.IsNullOrEmpty(x.PhoneNumber))
                        {
                            Message xm = new Message();
                            xm.Title = ms.Title;
                            xm.Mail = Messagex;
                            xm.Recipient = x.PhoneNumber;
                            xm.Retries = 0; xm.NotificationStatus = NotificationStatus.NotSent; xm.NotificationType = NotificationType.SMS;
                            _context.Messages.Add(xm);

                            countitem++;
                        }
                    }
                }
                await _context.SaveChangesAsync();
            }


            TempData["count"] = countitem + " emails generated and ready to start sending in few seconds";


            return Page();
        }


    }
}
