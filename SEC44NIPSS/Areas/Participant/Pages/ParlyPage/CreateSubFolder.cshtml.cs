using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SEC44NIPSS.Data;
using SEC44NIPSS.Data.Model;

namespace SEC44NIPSS.Areas.Participant.Pages.ParlyPage
{
    [Authorize]

    public class CreateSubFolderModel : PageModel
    {
        private readonly SEC44NIPSS.Data.NIPSSDbContext _context;

        public CreateSubFolderModel(SEC44NIPSS.Data.NIPSSDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public long? CatId { get; set; }

        [BindProperty]
        public ParlyReportCategory ParlyReportCategory { get; set; }
        public async Task<IActionResult> OnGetAsync(long id)
        {
            //ViewData["ParlyReportCategoryId"] = new SelectList(_context.ParlyReportCategories, "Id", "Id");
            //return Page();

            ParlyReportCategory = await _context.ParlyReportCategories.FirstOrDefaultAsync(x => x.Id == id);
            if(ParlyReportCategory == null)
            {
                return RedirectToPage("./Index");
            }
            
            return Page();
        }

        [BindProperty]
        public ParlyReportSubCategory ParlyReportSubCategory { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.ParlyReportSubCategories.Add(ParlyReportSubCategory);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Documents", new { id = ParlyReportSubCategory.ParlyReportCategoryId });
        }
    }
}
