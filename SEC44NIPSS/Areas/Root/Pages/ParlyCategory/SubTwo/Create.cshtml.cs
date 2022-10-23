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

namespace SEC44NIPSS.Areas.Root.Pages.ParlyCategory.SubTwo
{
    public class CreateModel : PageModel
    {
        private readonly SEC44NIPSS.Data.NIPSSDbContext _context;

        public CreateModel(SEC44NIPSS.Data.NIPSSDbContext context)
        {
            _context = context;
        }
        public long Id { get; set; }
        public ParlyReportSubCategory ParlyReportSubCategory { get; set; }
        public async Task<IActionResult> OnGet(long id)
        {
            ParlyReportSubCategory = await _context.ParlyReportSubCategories.Include(x=>x.ParlyReportCategory).FirstOrDefaultAsync(x => x.Id == id);
            if (ParlyReportSubCategory == null)
            {
                return RedirectToPage("/ParlyCategory/Index");
            }

            return Page();
        }

        [BindProperty]
        public ParlySubTwoCategory ParlySubTwoCategory { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.ParlySubTwoCategories.Add(ParlySubTwoCategory);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Folders", new { id = ParlySubTwoCategory.ParlyReportSubCategoryId });
        }
    }
}
