using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SEC44NIPSS.Data;
using SEC44NIPSS.Data.Model;

namespace SEC44NIPSS.Areas.Admin.Pages.ProfileAccount
{
    [Authorize(Roles = "mSuperAdmin,Admin")]

    public class EditModel : PageModel
    {
        private readonly SEC44NIPSS.Data.NIPSSDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public EditModel(SEC44NIPSS.Data.NIPSSDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [BindProperty]
        public Profile Profile { get; set; }

        [BindProperty]
        public string Email { get; set; }

        [BindProperty]
        public long StudyGroupId { get; set; }


        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Profile = await _context.Profiles
                .Include(p => p.User)
                .Include(x => x.StudyGroupMemeber)
                                           .ThenInclude(x => x.StudyGroup)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (Profile == null)
            {
                return NotFound();
            }
            ViewData["AlumniId"] = new SelectList(_context.Alumnis, "Id", "Title");
            ViewData["StudyGroupId"] = new SelectList(_context.StudyGroups, "Id", "Title");

            ViewData["UserId"] = new SelectList(_context.Users.OrderByDescending(x => x.Email), "Id", "Email");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            //if (!ModelState.IsValid)
            //{
            //    return Page();
            //}

            _context.Attach(Profile).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProfileExists(Profile.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }


            try
            {
                if (!String.IsNullOrEmpty(Email))
                {
                    var user = await _userManager.FindByIdAsync(Profile.UserId);
                    var xuser = await _userManager.GenerateChangeEmailTokenAsync(user, Email);
                    var chemail = await _userManager.ChangeEmailAsync(user, Email, xuser);
                    if (chemail.Succeeded)
                    {
                        TempData["er"] = "Email Changed";

                    }
                    else
                    {
                        return Page();
                    }
                }
            }
            catch (Exception c)
            {

            }

            try
            {
                if (StudyGroupId > 0)
                {
                    var m = await _context.StudyGroupMemebers.FirstOrDefaultAsync(x => x.ProfileId == Profile.Id);
                    m.StudyGroupId = StudyGroupId;
                    _context.Attach(Profile).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                    TempData["errr"] = "Group Changed";
                }
            }
            catch (Exception d)
            {

            }
            TempData["err"] = "Account Updated";
            return RedirectToPage("./Result");
        }

        private bool ProfileExists(long id)
        {
            return _context.Profiles.Any(e => e.Id == id);
        }
    }
}
