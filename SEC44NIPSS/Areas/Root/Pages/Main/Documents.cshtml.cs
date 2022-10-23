using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SEC44NIPSS.Data;
using SEC44NIPSS.Data.Model;

namespace SEC44NIPSS.Areas.Root.Pages.Main
{
    [Authorize]
    public class DocumentsModel : PageModel
    {
        private readonly SEC44NIPSS.Data.NIPSSDbContext _context;

        public DocumentsModel(SEC44NIPSS.Data.NIPSSDbContext context)
        {
            _context = context;
        }

        public IList<ParlyReportCategory> ParlyReportCategories { get; set; }
        public IList<ParlyReportCategory> ParlyFolder { get; set; }
        public IQueryable<string> Events { get; set; }
        

         
        public async Task<IActionResult> OnGetAsync()
        {
          

            ParlyReportCategories = await _context.ParlyReportCategories.Include(x=>x.ParlyReportSubCategories).Include(x=>x.ParlyReportDocuments).Where(x => x.Show == true && x.FolderType == FolderType.Main).ToListAsync();
            ParlyFolder = await _context.ParlyReportCategories.Include(x => x.ParlyReportSubCategories).Include(x => x.ParlyReportDocuments).Where(x => x.Show == true && x.FolderType == FolderType.Parly).ToListAsync();
           
            return Page();
        }

    }
}
