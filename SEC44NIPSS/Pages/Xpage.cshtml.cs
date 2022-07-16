using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SEC44NIPSS.Data;
using SEC44NIPSS.Data.Model;

namespace SEC44NIPSS.Pages
{
    public class XpageModel : PageModel
    {
        private readonly SEC44NIPSS.Data.NIPSSDbContext _context;

        public XpageModel(SEC44NIPSS.Data.NIPSSDbContext context)
        {
            _context = context;
        }

        public Alumni Alumni { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
           


            Alumni = await _context.Alumnis.FirstOrDefaultAsync(m => m.Id == 1);

            if (Alumni == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
