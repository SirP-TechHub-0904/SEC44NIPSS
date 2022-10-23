using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SEC44NIPSS.Data;
using SEC44NIPSS.Data.Model;

namespace SEC44NIPSS.Areas.Root.Pages.ParlyCategory.SubTwo
{
    public class FoldersModel : PageModel
    {
        private readonly SEC44NIPSS.Data.NIPSSDbContext _context;

        public FoldersModel(SEC44NIPSS.Data.NIPSSDbContext context)
        {
            _context = context;
        }

        public IList<ParlySubTwoCategory> ParlySubTwoCategory { get;set; }
        public ParlyReportSubCategory ParlyReportSubCategory { get; set; }

        public async Task OnGetAsync(long id)
        {
            ParlySubTwoCategory = await _context.ParlySubTwoCategories
                .Include(p => p.ParlyReportSubCategory)
                 .Include(p => p.ParlySubThreeCategories).Where(x=>x.ParlyReportSubCategoryId == id).OrderBy(x => x.SortOrder).ToListAsync();

            ParlyReportSubCategory = await _context.ParlyReportSubCategories.Include(x=>x.ParlyReportCategory).FirstOrDefaultAsync(x => x.Id == id);

        }
    }
}
