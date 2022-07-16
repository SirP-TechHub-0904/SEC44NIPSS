using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SEC44NIPSS.Data;
using SEC44NIPSS.Data.Model;

namespace SEC44NIPSS.Pages.Web
{
    public class NipssStaffModel : PageModel
    {
        private readonly SEC44NIPSS.Data.NIPSSDbContext _context;

        public NipssStaffModel(SEC44NIPSS.Data.NIPSSDbContext context)
        {
            _context = context;
        }

        public IList<NipssStaff> NipssStaff { get;set; }

        public async Task OnGetAsync()
        {
            NipssStaff = await _context.NipssStaff
                .Include(n => n.Profile).ToListAsync();
        }
    }
}
