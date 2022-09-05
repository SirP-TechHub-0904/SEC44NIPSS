using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SEC44NIPSS.Data.Model;

namespace SEC44NIPSS.Areas.Participant.Pages.ParlyPage
{
    public class DocumentsModel : PageModel
    {
        private readonly SEC44NIPSS.Data.NIPSSDbContext _context;

        public DocumentsModel(SEC44NIPSS.Data.NIPSSDbContext context)
        {
            _context = context;
        }

        public IList<ParlyReportDocument> ParlyReportDocuments { get; set; }
        public IList<ParlyReportCategory> ParlyReportCategoryList { get; set; }
        public ParlyReportCategory ParlyReportCategory { get; set; }

        public async Task OnGetAsync(long id)
        {
            ParlyReportDocuments = await _context.ParlyReportDocuments.Include(x => x.ParlyReportCategory).Include(x => x.Profile).Where(x => x.ParlyReportCategoryId == id).OrderByDescending(x => x.Date).ToListAsync();
            ParlyReportCategoryList = await _context.ParlyReportCategories.ToListAsync();

            ParlyReportCategory = await _context.ParlyReportCategories.FirstOrDefaultAsync(x => x.Id == id);


        }
    }
}
