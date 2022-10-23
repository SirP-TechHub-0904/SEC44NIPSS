using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using SEC44NIPSS.Data.Model;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.Xml.Linq;
using System;

namespace SEC44NIPSS.Areas.Identity.Pages.Roles
{
    public class IndexModel : PageModel
    {
        private readonly SEC44NIPSS.Data.NIPSSDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public IndexModel(SEC44NIPSS.Data.NIPSSDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult OnGet()
        {
           
            return Page();
        }

      
        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }
            public string Password { get; set; }
            public string Phone { get; set; }
            public string Role { get; set; }
            public string Name { get; set; }



        }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
        
            var user = new IdentityUser { UserName = Input.Email, Email = Input.Email, EmailConfirmed = true, PhoneNumber = Input.Phone };
            var result = await _userManager.CreateAsync(user, Input.Password);
            if (result.Succeeded)
            {
                TempData["success1"] = "account created";
                Profile profile = new Profile();
                profile.UserId = user.Id;
                profile.DateRegistered = DateTime.UtcNow.AddHours(1);
                profile.FullName = Input.Name;

                _context.Profiles.Add(profile);
                await _context.SaveChangesAsync();
                await _userManager.AddToRoleAsync(user, Input.Role);
            }
            TempData["success"] = "success";
            return RedirectToPage("./Index");
        }

    }

}
