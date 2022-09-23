using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SEC44NIPSS.Data;
using SEC44NIPSS.Data.Model;

namespace SEC44NIPSS.Pages.Web
{
    public class ParlyDocListModel : PageModel
    {
   
        private readonly SEC44NIPSS.Data.NIPSSDbContext _context;

        public ParlyDocListModel(SEC44NIPSS.Data.NIPSSDbContext context)
        {
            _context = context;
        }

        public IList<ParlyReportDocument> ParlyReportDocuments { get; set; }
        public IList<ParlyReportCategory> ParlyReportCategoryList { get; set; }
        public ParlyReportCategory ParlyReportCategory { get; set; }
        public IList<ParlyReportSubCategory> SubCategory { get; set; }
        public IList<ParlySubTwoCategory> SubTwoCategory { get; set; }
        public IList<ParlySubThreeCategory> SubThreeCategory { get; set; }
        public ParlyReportSubCategory ParlyReportSubCategory { get; set; }
        public ParlySubTwoCategory ParlySubTwoCategory { get; set; }
        public ParlySubThreeCategory ParlySubThreeCategory { get; set; }

        public bool SubFolder { get; set; }
        public bool SubCategoryCheck { get; set; }
        public bool SubTwoCategoryCheck { get; set; }
        public bool SubThreeCategoryCheck { get; set; }
        public bool SubFourCategoryCheck { get; set; }

        public long? SubId { get; set; }

        public async Task OnGetAsync(long? f1, long? f2, long? f3, long? f4)
        {

            if(f1 != null)
            {
                SubCategory = await _context.ParlyReportSubCategories.Where(x => x.ParlyReportCategoryId == f1).OrderBy(x=>x.SortOrder).ToListAsync();
                ParlyReportDocuments = await _context.ParlyReportDocuments.Include(x => x.ParlyReportSubCategory).Include(x => x.ParlyReportCategory).Include(x => x.Profile).Where(x => x.ParlyReportCategoryId == f1 && x.ParlyReportSubCategoryId == null).OrderByDescending(x => x.Date).ToListAsync();
                if (SubCategory.Count() == 0 && ParlyReportDocuments.Count() == 0)
                {
                    TempData["msg"] = "No File Found";
                }
                ParlyReportCategory = await _context.ParlyReportCategories.FirstOrDefaultAsync(x => x.Id == f1);
                SubCategoryCheck = true;
            }

            if (f2 != null)
            {
                SubTwoCategory = await _context.ParlySubTwoCategories.Where(x => x.ParlyReportSubCategoryId == f2).OrderBy(x => x.SortOrder).ToListAsync();
                ParlyReportDocuments = await _context.ParlyReportDocuments.Include(x => x.ParlyReportSubCategory).Include(x => x.ParlyReportCategory).Include(x => x.ParlySubTwoCategory).Include(x => x.Profile).Where(x => x.ParlyReportSubCategoryId == f2 && x.ParlySubTwoCategoryId == null && x.ParlyReportCategoryId == null).OrderByDescending(x => x.Date).ToListAsync();
                if (SubTwoCategory.Count() == 0 && ParlyReportDocuments.Count() == 0)
                {
                    TempData["msg"] = "No File Found";
                }
                ParlyReportSubCategory = await _context.ParlyReportSubCategories.FirstOrDefaultAsync(x => x.Id == f2);
                SubTwoCategoryCheck = true;
            }

            if (f3 != null)
            {
                SubThreeCategory = await _context.ParlySubThreeCategories.Where(x => x.ParlySubTwoCategoryId == f3).OrderBy(x => x.SortOrder).ToListAsync();
                ParlyReportDocuments = await _context.ParlyReportDocuments.Include(x => x.ParlyReportSubCategory).Include(x => x.ParlyReportCategory).Include(x => x.ParlySubTwoCategory).Include(x => x.Profile).Where(x => x.ParlySubTwoCategoryId == f3 && x.ParlyReportSubCategory == null && x.ParlyReportCategoryId == null).OrderByDescending(x => x.Date).ToListAsync();
                if (SubThreeCategory.Count() == 0 && ParlyReportDocuments.Count() == 0)
                {
                    TempData["msg"] = "No File Found";
                }
                ParlySubTwoCategory = await _context.ParlySubTwoCategories.FirstOrDefaultAsync(x => x.Id == f3);
                SubThreeCategoryCheck = true;
            }

            if (f4 != null)
            {
                ParlyReportDocuments = await _context.ParlyReportDocuments.Include(x => x.ParlyReportSubCategory).Include(x => x.ParlyReportCategory).Include(x => x.ParlySubTwoCategory).Include(x => x.Profile).Where(x => x.ParlySubThreeCategoryId == f4 && x.ParlySubTwoCategoryId == null && x.ParlyReportCategoryId == null).OrderByDescending(x => x.Date).ToListAsync();
                if (ParlyReportDocuments.Count() == 0)
                {
                    TempData["msg"] = "No File Found";
                }
                ParlySubThreeCategory = await _context.ParlySubThreeCategories.FirstOrDefaultAsync(x => x.Id == f4);
                SubFourCategoryCheck = true;
            }


            ParlyReportCategoryList = await _context.ParlyReportCategories.ToListAsync();



        }
    }
}
