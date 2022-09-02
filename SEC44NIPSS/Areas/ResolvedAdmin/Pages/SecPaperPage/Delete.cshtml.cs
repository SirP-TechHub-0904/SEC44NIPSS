using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SEC44NIPSS.Data;
using SEC44NIPSS.Data.Model;

namespace SEC44NIPSS.Areas.ResolvedAdmin.Pages.SecPaperPage
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
        public SecPaper SecPaper { get; set; }

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            SecPaper = await _context.SecPapers
                .Include(s => s.Alumni)
                .Include(s => s.DocumentCategory).FirstOrDefaultAsync(m => m.Id == id);

            if (SecPaper == null)
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

            SecPaper = await _context.SecPapers.FindAsync(id);

            if (SecPaper != null)
            {
                var fileDbPathName = $"/Document2022/".Trim();

               string filePath = $"{_hostingEnv.WebRootPath}".Trim();
                var fullPath = filePath + SecPaper.Powerpoint;

                if (System.IO.File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                }

                var fullPath1 = filePath + SecPaper.Report;

                if (System.IO.File.Exists(fullPath1))
                {
                    System.IO.File.Delete(fullPath1);
                }

                var fullPath2 = filePath + SecPaper.Script;

                if (System.IO.File.Exists(fullPath2))
                {
                    System.IO.File.Delete(fullPath2);
                }


                _context.SecPapers.Remove(SecPaper);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("/Main/Details", new { id = SecPaper.AlumniId });
        }
    }
}
