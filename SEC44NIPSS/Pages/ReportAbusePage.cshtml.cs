using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using SEC44NIPSS.Data;
using SEC44NIPSS.Data.Model;

namespace SEC44NIPSS.Pages
{
    public class ReportAbusePageModel : PageModel
    {
        private readonly SEC44NIPSS.Data.NIPSSDbContext _context;

        public ReportAbusePageModel(SEC44NIPSS.Data.NIPSSDbContext context)
        {
            _context = context;
        }
        [BindProperty]
        public long SS { get; set; }

        [BindProperty]
        public string LL { get; set; }
        public IActionResult OnGet(long x,string l=null)
        {
            SS = x;
            LL = l;
            return Page();
        }

        [BindProperty]
        public ReportAbuse ReportAbuse { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.ReportAbuses.Add(ReportAbuse);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
