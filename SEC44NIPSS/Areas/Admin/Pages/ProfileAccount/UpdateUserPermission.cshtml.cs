using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace SEC44NIPSS.Areas.Admin.Pages.ProfileAccount
{
    [Authorize(Roles = "mSuperAdmin")]

    public class UpdateUserPermissionModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly SEC44NIPSS.Data.NIPSSDbContext _context;



        public UpdateUserPermissionModel(
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager,
            SignInManager<IdentityUser> signInManager, Data.NIPSSDbContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _context = context;
        }


        public IList<string> Roles { get; set; }
        public IList<string> UserRoles { get; set; }
        public IList<string> RemainingUserRoles { get; set; }
        public IdentityUser UserInfo { get; set; }

        [BindProperty]
        public string UserId { get; set; }
        [BindProperty]
        public string Fullname { get; set; }

        public async Task<IActionResult> OnGetAsync(string uid, string fullname)
        {
            if (uid == null)
            {
                return NotFound();
            }
            Fullname = fullname;
            Roles = await _roleManager.Roles.Where(x => x.Name != "mSuperAdmin").Select(x => x.Name).ToListAsync();

            UserInfo = await _userManager.FindByIdAsync(uid);
            UserRoles = await _userManager.GetRolesAsync(UserInfo);
            var profile = await _context.Profiles.FirstOrDefaultAsync(x => x.UserId == uid);
            //profile.Roles = await _account.FetchUserRoles(UserInfo.Id);
            //await _account.Update(profile);
            
            var RemainingRoles = Roles.Except(UserRoles);
            RemainingUserRoles = RemainingRoles.ToList();

            if (UserInfo == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id, string fullname)
        {
            Fullname = fullname;
            var role = await _roleManager.FindByIdAsync(id);
            var user = await _userManager.FindByIdAsync(UserId);
            var checkuserroles = await _userManager.IsInRoleAsync(user, role.Name);
            if (checkuserroles == true)
            {
                try
                {
                    await _userManager.RemoveFromRoleAsync(user, role.Name);
                }
                catch (Exception d) { }
            }
            else
            {
                try
                {
                    await _userManager.AddToRoleAsync(user, role.Name);
                }
                catch (Exception d) { }
            }
            return RedirectToPage("./UpdateUserPermission", new { uid = user.Id, fullname = Fullname });
        }
           
    }
}
