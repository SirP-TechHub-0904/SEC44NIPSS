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
    public class InnerFolderThreeModel : PageModel
    {
        private readonly SEC44NIPSS.Data.NIPSSDbContext _context;

        public InnerFolderThreeModel(SEC44NIPSS.Data.NIPSSDbContext context)
        {
            _context = context;
        }
        public IList<ParlySubTwoCategory> ParlySubTwoCategory { get; set; }
        public IList<ParlyReportDocument> ParlyReportDocuments { get; set; }
        public ParlyReportSubCategory ParlyReportSubCategory { get; set; }


        public async Task OnGetAsync(long id)
        {
            ParlySubTwoCategory = await _context.ParlySubTwoCategories
                .Include(p => p.ParlyReportSubCategory).Include(p => p.ParlyReportDocuments).Include(p => p.ParlySubThreeCategories).Where(x => x.ParlyReportSubCategoryId == id).OrderBy(x => x.SortOrder).ToListAsync();

            ParlyReportSubCategory = await _context.ParlyReportSubCategories.Include(x=>x.ParlyReportCategory).FirstOrDefaultAsync(x => x.Id == id);

            ParlyReportDocuments = await _context.ParlyReportDocuments.Where(x=>x.ParlyReportCategoryId==null && x.ParlyReportSubCategoryId == id && x.ParlySubTwoCategoryId == null && x.ParlySubThreeCategoryId == null).OrderBy(x => x.SortOrder).ToListAsync();
        }
    }
}
