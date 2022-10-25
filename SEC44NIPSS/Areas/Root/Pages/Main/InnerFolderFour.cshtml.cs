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
    public class InnerFolderFourModel : PageModel
    {
        private readonly SEC44NIPSS.Data.NIPSSDbContext _context;

        public InnerFolderFourModel(SEC44NIPSS.Data.NIPSSDbContext context)
        {
            _context = context;
        }
        public IList<ParlySubThreeCategory> ParlySubThreeCategory { get; set; }
        public IList<ParlyReportDocument> ParlyReportDocuments { get; set; }
        public ParlySubTwoCategory ParlySubTwoCategory { get; set; }


        public async Task OnGetAsync(long id)
        {
            ParlySubThreeCategory = await _context.ParlySubThreeCategories
                .Include(p => p.ParlySubTwoCategory).Include(p => p.ParlyReportDocuments).Where(x => x.ParlySubTwoCategoryId == id).OrderBy(x => x.SortOrder).ToListAsync();

            ParlySubTwoCategory = await _context.ParlySubTwoCategories.Include(x=>x.ParlyReportSubCategory).ThenInclude(x=>x.ParlyReportCategory).FirstOrDefaultAsync(x => x.Id == id);

            ParlyReportDocuments = await _context.ParlyReportDocuments.Where(x=>x.ParlyReportCategoryId==null && x.ParlyReportSubCategoryId == null && x.ParlySubTwoCategoryId == id && x.ParlySubThreeCategoryId == null).OrderBy(x => x.SortOrder).ToListAsync();
        }
    }
}
