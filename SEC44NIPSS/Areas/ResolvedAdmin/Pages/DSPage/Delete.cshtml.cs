using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SEC44NIPSS.Data;
using SEC44NIPSS.Data.Model;

namespace SEC44NIPSS.Areas.ResolvedAdmin.Pages.DSPage
{
    public class DeleteModel : PageModel
    {
        private readonly SEC44NIPSS.Data.NIPSSDbContext _context;

        public DeleteModel(SEC44NIPSS.Data.NIPSSDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public DirectingStaff DirectingStaff { get; set; }

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            DirectingStaff = await _context.DirectingStaffs
                .Include(d => d.Alumni)
                .Include(d => d.Profile)
                .Include(d => d.StudyGroup).FirstOrDefaultAsync(m => m.Id == id);

            if (DirectingStaff == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            DirectingStaff = await _context.DirectingStaffs.FindAsync(id);

            if (DirectingStaff != null)
            {
                _context.DirectingStaffs.Remove(DirectingStaff);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("/Main/Details", new { id = DirectingStaff.AlumniId });
        }
    }
}
