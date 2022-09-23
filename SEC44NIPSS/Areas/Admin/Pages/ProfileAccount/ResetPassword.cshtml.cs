using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace SEC44NIPSS.Areas.Admin.Pages.ProfileAccount
{
       public class ResetPasswordModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ILogger<ResetPasswordModel> _logger;
        private readonly SEC44NIPSS.Data.NIPSSDbContext _context;


        public ResetPasswordModel(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            ILogger<ResetPasswordModel> logger, Data.NIPSSDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _context = context;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        public class InputModel
        {


            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "New password")]
            public string NewPassword { get; set; }
            

        }

        [BindProperty]
        public string UIDD { get; set; }

        [BindProperty]
        public string PIX { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
          
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
            UIDD = user.Id;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _userManager.FindByIdAsync(UIDD);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
            var profile = await _context.Profiles.FirstOrDefaultAsync(x => x.UserId == user.Id);

            var changePasswordResult = await _userManager.RemovePasswordAsync(user);
            if (!changePasswordResult.Succeeded)
            {
                foreach (var error in changePasswordResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return Page();
            }
            var NewchangePasswordResult = await _userManager.AddPasswordAsync(user, Input.NewPassword);
            if (NewchangePasswordResult.Succeeded)
            {

                var passcheck = await _userManager.CheckPasswordAsync(user, Input.NewPassword);

                profile.PsChange = true;
                profile.PXI = Input.NewPassword;
                _context.Attach(profile).State = EntityState.Modified;

                await _context.SaveChangesAsync();
                _logger.LogInformation("User changed their password successfully.");
                StatusMessage = "Your password has been changed.";

                return RedirectToPage("./ResetPassword", new { id = user.Id });
            }
            return RedirectToPage("./ResetPassword", new { id = user.Id });

        }
    }

}
