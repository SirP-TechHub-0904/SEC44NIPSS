using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using SEC44NIPSS.Data;
using SEC44NIPSS.Data.Model;

namespace SEC44NIPSS.Areas.Participant.Pages.ParlyPage
{
    [Authorize]

    public class CreateFolderModel : PageModel
    {
        private readonly SEC44NIPSS.Data.NIPSSDbContext _context;

        public CreateFolderModel(SEC44NIPSS.Data.NIPSSDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public ParlyReportCategory ParlyReportCategory { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.ParlyReportCategories.Add(ParlyReportCategory);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
