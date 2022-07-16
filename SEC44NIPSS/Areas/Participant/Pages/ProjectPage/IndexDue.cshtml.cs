using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SEC44NIPSS.Data.Model;

namespace SEC44NIPSS.Areas.Participant.Pages.ProjectPage
{
    [Authorize]
    public class IndexDueModel : PageModel
    {
        private readonly SEC44NIPSS.Data.NIPSSDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public IndexDueModel(SEC44NIPSS.Data.NIPSSDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public LegacyProject LegacyProject { get; set; }
        [BindProperty]
        public string Email { get; set; }
        

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
            var check = await _context.LegacyProjectAnswers.Where(x=>x.VotingType == VotingType.Dues).Select(x=>x.Email).ToListAsync();
            if (check.Contains(user.Email))
            {
                TempData["alert"] = "You have Voted Already";
                return RedirectToPage("./ResultDue");
            }
            var profile = await _context.Profiles.FirstOrDefaultAsync(x => x.UserId == user.Id);
            Email = user.Email;
           
            LegacyProject = await _context.LegacyProjects.FirstOrDefaultAsync();

            return Page();
        }
        [BindProperty]
        public LegacyProjectAnswer LegacyProjectAnswer { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            LegacyProjectAnswer.VotingType = VotingType.Dues;
            LegacyProjectAnswer.Email = Email;
            _context.LegacyProjectAnswers.Add(LegacyProjectAnswer);
            await _context.SaveChangesAsync();

            return RedirectToPage("./ResultDue");
        }
    }

}
