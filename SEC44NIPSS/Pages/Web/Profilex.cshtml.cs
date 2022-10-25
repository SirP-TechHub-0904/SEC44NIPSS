using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SEC44NIPSS.Data.Model;

namespace SEC44NIPSS.Pages.Web
{
    //[Authorize]
    public class ProfilexModel : PageModel
    {
        private readonly SEC44NIPSS.Data.NIPSSDbContext _context;
                
        public ProfilexModel(SEC44NIPSS.Data.NIPSSDbContext context)
        {
            _context = context;
        }
        public Alumni Alumni { get; set; }
        public IList<Profile> Profile { get; set; }

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            //Profile = await _context.Profiles
            //    .Include(p => p.User).Where(x => x.DontShow == false).Where(x => x.AlumniId == id).ToListAsync();

            Alumni = await _context.Alumnis
                .Include(x => x.Participants)
                .ThenInclude(x => x.Profile)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (Alumni == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
