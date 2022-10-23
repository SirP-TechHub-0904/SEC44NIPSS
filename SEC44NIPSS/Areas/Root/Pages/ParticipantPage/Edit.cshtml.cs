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

namespace SEC44NIPSS.Areas.Root.Pages.ParticipantPage
{
    public class EditModel : PageModel
    {
        private readonly SEC44NIPSS.Data.NIPSSDbContext _context;

        public EditModel(SEC44NIPSS.Data.NIPSSDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public SecParticipant Participant { get; set; }

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Participant = await _context.Participants
                .Include(p => p.Alumni)
                .Include(p => p.StudyGroup)
                .Include(p => p.Profile).FirstOrDefaultAsync(m => m.Id == id);

            if (Participant == null)
            {
                return NotFound();
            }
            var prof = await _context.Profiles.ToListAsync();

            var output = prof.Select(x => new ProfileWithTitle
            {
                Id = x.Id,
                Fullname = x.Title + " " + x.FullName
            });

            ViewData["AlumniId"] = new SelectList(_context.Alumnis, "Id", "Title");
            ViewData["StudyGroupId"] = new SelectList(_context.StudyGroups.Where(x => x.AlumniId == Participant.AlumniId), "Id", "Title");
            ViewData["ProfileId"] = new SelectList(output, "Id", "Fullname");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Participant).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ParticipantExists(Participant.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("/Main/Details", new { id = Participant.AlumniId });
        }

        private bool ParticipantExists(long id)
        {
            return _context.Participants.Any(e => e.Id == id);
        }
    }
}
