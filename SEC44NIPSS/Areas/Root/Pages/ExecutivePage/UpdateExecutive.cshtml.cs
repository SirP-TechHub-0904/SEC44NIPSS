using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SEC44NIPSS.Data;
using SEC44NIPSS.Data.Dtos;
using SEC44NIPSS.Data.Model;

namespace SEC44NIPSS.Areas.Root.Pages.ExecutivePage
{
    public class UpdateExecutiveModel : PageModel
    {
        private readonly SEC44NIPSS.Data.NIPSSDbContext _context;

        public UpdateExecutiveModel(SEC44NIPSS.Data.NIPSSDbContext context)
        {
            _context = context;
        }


        public async Task<IActionResult> OnGet(long id)
        {

            var al = await _context.Alumnis.FindAsync(id);
            if (al == null)
            {
                return NotFound();
            }
            var prof = await _context.Profiles.ToListAsync();
            var part = await _context.Participants.Where(x => x.AlumniId == al.Id).Select(x => x.ProfileId).ToListAsync();
            var output = prof.Where(x => !part.Contains(x.Id)).Select(x => new ProfileWithTitle
            {
                Id = x.Id,
                Fullname = x.Title + " " + x.FullName
            });

            TempData["title"] = al.Title;
            TempData["id"] = al.Id;
            ViewData["ProfileId"] = new SelectList(output, "Id", "Fullname");
            return Page();
        }

        [BindProperty]
        public Executive Executive { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Executive.Add(Executive);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Main/Details", new { id = Executive.AlumniId });
        }
    }
}
