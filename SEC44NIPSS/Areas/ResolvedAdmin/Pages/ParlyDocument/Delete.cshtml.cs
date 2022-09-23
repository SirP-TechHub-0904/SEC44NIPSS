using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SEC44NIPSS.Data;
using SEC44NIPSS.Data.Model;

namespace SEC44NIPSS.Areas.ResolvedAdmin.Pages.ParlyDocument
{
    public class DeleteModel : PageModel
    {
        private readonly SEC44NIPSS.Data.NIPSSDbContext _context;
        private readonly IHostingEnvironment _hostingEnv;


        public DeleteModel(SEC44NIPSS.Data.NIPSSDbContext context, IHostingEnvironment hostingEnv)
        {
            _context = context;
            _hostingEnv = hostingEnv;
        }

        [BindProperty]
        public ParlyReportDocument ParlyReportDocument { get; set; }

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ParlyReportDocument = await _context.ParlyReportDocuments
                .Include(p => p.ParlyReportCategory)
                .Include(p => p.Profile).FirstOrDefaultAsync(m => m.Id == id);

            if (ParlyReportDocument == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ParlyReportDocument = await _context.ParlyReportDocuments.FindAsync(id);

            if (ParlyReportDocument != null)
            {

                //var LagefileDbPathName = $"/GalleryLargeImage/".Trim();

                var LargefilePath = $"{_hostingEnv.WebRootPath}{ParlyReportDocument.Document}".Trim();
                var fullPath = LargefilePath;

                if (System.IO.File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                }

                _context.ParlyReportDocuments.Remove(ParlyReportDocument);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
