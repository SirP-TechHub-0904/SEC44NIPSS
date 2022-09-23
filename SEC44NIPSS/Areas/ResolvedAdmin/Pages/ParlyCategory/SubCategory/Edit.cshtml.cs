using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SEC44NIPSS.Data;
using SEC44NIPSS.Data.Model;

namespace SEC44NIPSS.Areas.ResolvedAdmin.Pages.ParlyCategory.SubCategory
{
    public class EditModel : PageModel
    {
        private readonly SEC44NIPSS.Data.NIPSSDbContext _context;

        public EditModel(SEC44NIPSS.Data.NIPSSDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ParlyReportSubCategory ParlyReportSubCategory { get; set; }

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ParlyReportSubCategory = await _context.ParlyReportSubCategories
                .Include(p => p.ParlyReportCategory).FirstOrDefaultAsync(m => m.Id == id);

            if (ParlyReportSubCategory == null)
            {
                return NotFound();
            }
           ViewData["ParlyReportCategoryId"] = new SelectList(_context.ParlyReportCategories, "Id", "Title");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(ParlyReportSubCategory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ParlyReportSubCategoryExists(ParlyReportSubCategory.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Folders", new { id = ParlyReportSubCategory.ParlyReportCategoryId });
        }

        private bool ParlyReportSubCategoryExists(long id)
        {
            return _context.ParlyReportSubCategories.Any(e => e.Id == id);
        }
    }
}
