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

namespace SEC44NIPSS.Areas.ResolvedAdmin.Pages.ParlyCategory.SubTwo
{
    public class EditModel : PageModel
    {
        private readonly SEC44NIPSS.Data.NIPSSDbContext _context;

        public EditModel(SEC44NIPSS.Data.NIPSSDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ParlySubTwoCategory ParlySubTwoCategory { get; set; }

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ParlySubTwoCategory = await _context.ParlySubTwoCategories
                .Include(p => p.ParlyReportSubCategory).FirstOrDefaultAsync(m => m.Id == id);

            if (ParlySubTwoCategory == null)
            {
                return NotFound();
            }
           ViewData["ParlyReportSubCategoryId"] = new SelectList(_context.ParlyReportCategories, "Id", "Id");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(ParlySubTwoCategory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ParlySubTwoCategoryExists(ParlySubTwoCategory.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Folders", new { id = ParlySubTwoCategory.ParlyReportSubCategoryId });
        }

        private bool ParlySubTwoCategoryExists(long id)
        {
            return _context.ParlySubTwoCategories.Any(e => e.Id == id);
        }
    }
}
