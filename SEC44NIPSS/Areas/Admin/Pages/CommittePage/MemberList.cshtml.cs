using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SEC44NIPSS.Data.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SEC44NIPSS.Areas.Admin.Pages.CommittePage
{
     public class MembersListModel : PageModel
    {
        private readonly SEC44NIPSS.Data.NIPSSDbContext _context;

        public MembersListModel(SEC44NIPSS.Data.NIPSSDbContext context)
        {
            _context = context;
        }

        public IList<Committee> Committee { get; set; }

        public async Task<IActionResult> OnGetAsync(long id)
        {
            Committee = await _context.Committees
                .Include(c => c.CommitteeCategory)
                .Include(c => c.Profile).Where(x=>x.CommitteeCategoryId == id)
                .ToListAsync();

            return Page();
        }
    }
}
