using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SEC44NIPSS.Data;
using SEC44NIPSS.Data.Model;

namespace SEC44NIPSS.Areas.Admin.Pages.AlumniPage.ExecutivePage
{
    public class ExecutiveListModel : PageModel
    {
        private readonly SEC44NIPSS.Data.NIPSSDbContext _context;

        public ExecutiveListModel(SEC44NIPSS.Data.NIPSSDbContext context)
        {
            _context = context;
        }

        public IList<Executive> Executive { get;set; }
        public Alumni Alumni { get; set; }
        public async Task OnGetAsync(long id)
        {
            Executive = await _context.Executive
                .Include(e => e.Alumni)
                .Include(e => e.Profile)
                .Where(x=>x.AlumniId == id)
                .ToListAsync();

            Alumni = await _context.Alumnis.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
