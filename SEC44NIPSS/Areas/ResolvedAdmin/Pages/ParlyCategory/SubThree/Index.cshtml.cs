using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SEC44NIPSS.Data;
using SEC44NIPSS.Data.Model;

namespace SEC44NIPSS.Areas.ResolvedAdmin.Pages.ParlyCategory.SubThree
{
    public class IndexModel : PageModel
    {
        private readonly SEC44NIPSS.Data.NIPSSDbContext _context;

        public IndexModel(SEC44NIPSS.Data.NIPSSDbContext context)
        {
            _context = context;
        }

        public IList<ParlySubThreeCategory> ParlySubThreeCategory { get;set; }

        public async Task OnGetAsync()
        {
            ParlySubThreeCategory = await _context.ParlySubThreeCategories
                .Include(p => p.ParlySubTwoCategory).OrderBy(x => x.SortOrder).ToListAsync();
        }
    }
}
