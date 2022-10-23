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
    public class InnerFolderFiveModel : PageModel
    {
        private readonly SEC44NIPSS.Data.NIPSSDbContext _context;

        public InnerFolderFiveModel(SEC44NIPSS.Data.NIPSSDbContext context)
        {
            _context = context;
        }
        public IList<ParlyReportDocument> ParlyReportDocuments { get; set; }
        public ParlySubThreeCategory ParlySubThreeCategory { get; set; }


        public async Task OnGetAsync(long id)
        {
           
            ParlySubThreeCategory = await _context.ParlySubThreeCategories.FirstOrDefaultAsync(x => x.Id == id);

            ParlyReportDocuments = await _context.ParlyReportDocuments.Where(x=>x.ParlyReportCategoryId==null && x.ParlyReportSubCategoryId == null && x.ParlySubTwoCategoryId == null && x.ParlySubThreeCategoryId == id).OrderBy(x => x.SortOrder).ToListAsync();
        }
    }
}
