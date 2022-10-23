using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SEC44NIPSS.Data.Model;
using ServiceStack;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SEC44NIPSS.Areas.Root.Pages.Main
{
    [Authorize]
    public class InnerFolderTwoModel : PageModel
    {
        private readonly SEC44NIPSS.Data.NIPSSDbContext _context;

        public InnerFolderTwoModel(SEC44NIPSS.Data.NIPSSDbContext context)
        {
            _context = context;
        }
        public IList<ParlyReportSubCategory> ParlyReportSubCategory { get; set; }
        public IList<ParlyReportDocument> ParlyReportDocuments { get; set; }
        public ParlyReportCategory ParlyReportCategory { get; set; }


        public async Task OnGetAsync(long id)
        {
            ParlyReportSubCategory = await _context.ParlyReportSubCategories
                .Include(p => p.ParlyReportCategory).Include(p => p.ParlySubTwoCategories).Include(p => p.ParlyReportDocuments).Where(x => x.ParlyReportCategoryId == id).OrderBy(x => x.SortOrder).ToListAsync();

            ParlyReportCategory = await _context.ParlyReportCategories.FirstOrDefaultAsync(x => x.Id == id);

            ParlyReportDocuments = await _context.ParlyReportDocuments.Where(x=>x.ParlyReportCategoryId==id && x.ParlyReportSubCategoryId == null && x.ParlySubTwoCategoryId == null && x.ParlySubThreeCategoryId == null).OrderBy(x => x.SortOrder).ToListAsync();
        }
    }
}
