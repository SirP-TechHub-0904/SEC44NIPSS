using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace SEC44NIPSS.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class FirstsigninModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<FirstsigninModel> _logger;
        private readonly SEC44NIPSS.Data.NIPSSDbContext _context;


        public FirstsigninModel(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            ILogger<FirstsigninModel> logger, Data.NIPSSDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _context = context;
        }


        public async Task<IActionResult> OnGetAsync(string userId, string code)
        {

            if (!String.IsNullOrEmpty(code))
            {
                var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == userId);
                if (user != null)
                {
                    var profile = await _context.Profiles.FirstOrDefaultAsync(x => x.UserId == user.Id);
                    if (profile.PsChange == false)
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);

                        return RedirectToPage("/Account/Manage/PasswordChange", new { xic = profile.UserId, area= "Identity" });
                    }
                    else
                    {
                        TempData["alert"] = "Password already changed. Kindly enter your current password";
                        return RedirectToPage("/Account/Login",new { area = "Identity" });
                    }
                }

            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
