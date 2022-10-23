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

namespace SEC44NIPSS.Areas.Root.Pages.ParlyCategory.SubThree
{
    public class CreateModel : PageModel
    {
        private readonly SEC44NIPSS.Data.NIPSSDbContext _context;

        public CreateModel(SEC44NIPSS.Data.NIPSSDbContext context)
        {
            _context = context;
        }

        public long Id { get; set; }
        public ParlySubTwoCategory ParlySubTwoCategory { get; set; }
        public ParlyReportSubCategory ParlyReportSubCategory { get; set; }

        public async Task<IActionResult> OnGet(long id)
        {
            ParlySubTwoCategory = await _context.ParlySubTwoCategories.Include(x => x.ParlyReportSubCategory).FirstOrDefaultAsync(x => x.Id == id);
            if (ParlySubTwoCategory == null)
            {
                return RedirectToPage("/ParlyCategory/Index");
            }
            ParlyReportSubCategory = await _context.ParlyReportSubCategories.Include(x => x.ParlyReportCategory).FirstOrDefaultAsync(x => x.Id == ParlySubTwoCategory.Id);

            return Page();
        }

        [BindProperty]
        public ParlySubThreeCategory ParlySubThreeCategory { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.ParlySubThreeCategories.Add(ParlySubThreeCategory);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Folders", new { id = ParlySubThreeCategory.ParlySubTwoCategoryId });
        }
    }
}
