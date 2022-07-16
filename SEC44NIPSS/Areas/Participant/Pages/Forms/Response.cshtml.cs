using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SEC44NIPSS.Data;
using SEC44NIPSS.Data.Model;

namespace SEC44NIPSS.Areas.Participant.Pages.Forms
{
    public class ResponseModel : PageModel
    {
        private readonly SEC44NIPSS.Data.NIPSSDbContext _context;

        public ResponseModel(SEC44NIPSS.Data.NIPSSDbContext context)
        {
            _context = context;
        }

        public Questionner Questionner { get; set; }

        public async Task<IActionResult> OnGetAsync(string x = null)
        {
           

            Questionner = await _context.Questionners
                .Include(q => q.Profile).FirstOrDefaultAsync(m => m.LongLink == x);

            if (Questionner == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
