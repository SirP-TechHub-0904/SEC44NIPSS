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

namespace SEC44NIPSS.Areas.ResolvedAdmin.Pages.ParlyCategory.SubThree
{
    public class EditModel : PageModel
    {
        private readonly SEC44NIPSS.Data.NIPSSDbContext _context;

        public EditModel(SEC44NIPSS.Data.NIPSSDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ParlySubThreeCategory ParlySubThreeCategory { get; set; }

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ParlySubThreeCategory = await _context.ParlySubThreeCategories
                .Include(p => p.ParlySubTwoCategory).FirstOrDefaultAsync(m => m.Id == id);

            if (ParlySubThreeCategory == null)
            {
                return NotFound();
            }
           ViewData["ParlySubTwoCategoryId"] = new SelectList(_context.ParlySubTwoCategories, "Id", "Id");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(ParlySubThreeCategory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ParlySubThreeCategoryExists(ParlySubThreeCategory.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ParlySubThreeCategoryExists(long id)
        {
            return _context.ParlySubThreeCategories.Any(e => e.Id == id);
        }
    }
}
