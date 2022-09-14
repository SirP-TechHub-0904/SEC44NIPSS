using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SEC44NIPSS.Data.Model;

namespace SEC44NIPSS.Areas.Identity.Pages.Roles
{
    public class SeedAccountModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SEC44NIPSS.Data.NIPSSDbContext _context;

        public SeedAccountModel(
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager, Data.NIPSSDbContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
        }

        public string ReturnUrl { get; set; }

        public async Task<IActionResult> OnGetAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");

            var user = new IdentityUser { UserName = "jinmcever@xyz.com", Email = "jinmcever@xyz.com", PhoneNumber = "070000000000000", LockoutEnabled = false };
            var result = await _userManager.CreateAsync(user, "jinmcever@123");
            if (result.Succeeded)
            {
               
                    var role = new IdentityRole("mSuperAdmin");
                    await _roleManager.CreateAsync(role);
                    await _userManager.AddToRoleAsync(user, "mSuperAdmin");

                var role1 = new IdentityRole("Admin");
                await _roleManager.CreateAsync(role1);

                var role11 = new IdentityRole("Staff");
                await _roleManager.CreateAsync(role11);

                var role2 = new IdentityRole("Participant");
                await _roleManager.CreateAsync(role2);


                Profile profile = new Profile();
                profile.UserId = user.Id;
                profile.DateRegistered = DateTime.UtcNow.AddHours(1);
                profile.FullName = "Major Admin Account";

                _context.Profiles.Add(profile);
                await _context.SaveChangesAsync();
                return LocalRedirect(Url.Content("~/"));
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return LocalRedirect(Url.Content("~/"));
        }


    }

}
