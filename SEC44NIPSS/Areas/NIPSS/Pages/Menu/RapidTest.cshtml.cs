using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SEC44NIPSS.Data;
using SEC44NIPSS.Data.Model;

namespace SEC44NIPSS.Areas.NIPSS.Pages.Menu
{
    public class RapidTestModel : PageModel
    {
        private readonly SEC44NIPSS.Data.NIPSSDbContext _context;

        public RapidTestModel(SEC44NIPSS.Data.NIPSSDbContext context)
        {
            _context = context;
        }

        public IList<RapidTest> RapidTest { get;set; }

        public async Task OnGetAsync()
        {
            RapidTest = await _context.RapidTest.ToListAsync();
        }
    }
}
