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
    public class TermModel : PageModel
    {
        private readonly SEC44NIPSS.Data.NIPSSDbContext _context;

        public TermModel(SEC44NIPSS.Data.NIPSSDbContext context)
        {
            _context = context;
        }
       
        public IActionResult OnGet()
        {

            return Page();
        }
 }
}
