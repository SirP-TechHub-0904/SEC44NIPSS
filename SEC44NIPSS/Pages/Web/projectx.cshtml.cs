using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SEC44NIPSS.Data.Model;

namespace SEC44NIPSS.Pages.Web
{
    public class projectxModel : PageModel
    {
        private readonly SEC44NIPSS.Data.NIPSSDbContext _context;

        public projectxModel(SEC44NIPSS.Data.NIPSSDbContext context)
        {
            _context = context;
        }
        public Alumni Alumni { get; set; }
        public SecProject SecProject { get; set; }
        public IList<SecProjectExecutive> SecProjectExecutives { get; set; }

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            SecProject = await _context.secProjects.Include(x => x.SecProjectExecutives).FirstOrDefaultAsync(x => x.AlumniId == id);
            if (SecProject != null)
            {
                SecProjectExecutives = await _context.SecProjectExecutives.Include(x => x.Profile).Where(x => x.SecProjectId == SecProject.Id).ToListAsync();
            }
            Alumni = await _context.Alumnis
                .Include(x => x.SecProject)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (Alumni == null)
            {
                return NotFound();
            }
            return Page();
        }

    }
}
