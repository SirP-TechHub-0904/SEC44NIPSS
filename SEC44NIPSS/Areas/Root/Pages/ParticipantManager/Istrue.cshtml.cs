using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SEC44NIPSS.Data;
using SEC44NIPSS.Data.Model;

namespace SEC44NIPSS.Areas.Root.Pages.ParticipantManager
{
    public class IstrueModel : PageModel
    {
        private readonly SEC44NIPSS.Data.NIPSSDbContext _context;

        public IstrueModel(SEC44NIPSS.Data.NIPSSDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public SecParticipant SecParticipant { get; set; }

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            SecParticipant = await _context.Participants
                .Include(s => s.Alumni)
                .Include(s => s.Profile)
                .Include(s => s.StudyGroup).FirstOrDefaultAsync(m => m.Id == id);

            if (SecParticipant == null)
            {
                return NotFound();
            }
            if(SecParticipant.IsTrue == true)
            {
                SecParticipant.IsTrue = false;
            }
            else
            {
                SecParticipant.IsTrue = true;
            }
            _context.Attach(SecParticipant).State = EntityState.Modified;

                await _context.SaveChangesAsync();
            
            return Page();
        }

      

    }
}
