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

namespace SEC44NIPSS.Areas.ResolvedAdmin.Pages.ParticipantPage
{
    public class UpdateParticipantModel : PageModel
    {
        private readonly SEC44NIPSS.Data.NIPSSDbContext _context;

        public UpdateParticipantModel(SEC44NIPSS.Data.NIPSSDbContext context)
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
            var output = prof.Where(x=>!part.Contains(x.Id)).Select(x => new ProfileWithTitle
            {
                Id = x.Id,
                Fullname = x.Title + " " + x.FullName
            });

            TempData["title"] = al.Title;
            TempData["id"] = al.Id;
            ViewData["ProfileId"] = new SelectList(output, "Id", "Fullname");
            ViewData["StudyGroupId"] = new SelectList(_context.StudyGroups.Where(x => x.AlumniId == al.Id), "Id", "Title");
            return Page();

        }

        [BindProperty]
        public SecParticipant Participant { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Participants.Add(Participant);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Main/Details", new { id = Participant.AlumniId });
        }
    }
}
