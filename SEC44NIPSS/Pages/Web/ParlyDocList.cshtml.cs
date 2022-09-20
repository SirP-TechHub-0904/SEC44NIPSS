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
        public ParlyReportSubCategory ParlyReportSubCategory { get; set; }

        public bool SubFolder { get; set; }

        public long? SubId { get; set; }

        public async Task OnGetAsync(long id, string typefolder)
        {
            if (typefolder == "subfolder")
            {
                ParlyReportDocuments = await _context.ParlyReportDocuments.Include(x => x.ParlyReportCategory).Include(x => x.Profile).Where(x => x.ParlyReportSubCategoryId == id).OrderByDescending(x => x.Date).ToListAsync();
                SubId = id;
                ParlyReportSubCategory = await _context.ParlyReportSubCategories.FirstOrDefaultAsync(x => x.Id == id);

                ParlyReportCategory = await _context.ParlyReportCategories.Include(x => x.ParlyReportSubCategories).Include(x => x.ParlyReportDocuments).FirstOrDefaultAsync(x => x.Id == ParlyReportSubCategory.ParlyReportCategoryId);

            }
            else
            {
                ParlyReportDocuments = await _context.ParlyReportDocuments.Include(x => x.ParlyReportSubCategory).Include(x => x.ParlyReportCategory).Include(x => x.Profile).Where(x => x.ParlyReportCategoryId == id && x.ParlyReportSubCategoryId == null).OrderByDescending(x => x.Date).ToListAsync();
                ParlyReportSubCategory = await _context.ParlyReportSubCategories.FirstOrDefaultAsync(x => x.Id == id);
                SubFolder = true;
                ParlyReportCategory = await _context.ParlyReportCategories.Include(x => x.ParlyReportSubCategories).Include(x => x.ParlyReportDocuments).FirstOrDefaultAsync(x => x.Id == id);

            }
            ParlyReportCategoryList = await _context.ParlyReportCategories.ToListAsync();



        }
    }
}
