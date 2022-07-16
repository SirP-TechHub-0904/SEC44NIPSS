using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SEC44NIPSS.Data.Model;

namespace SEC44NIPSS.Areas.Participant.Pages.Dashboard
{
    public class LibraryModel : PageModel
    {
        private readonly SEC44NIPSS.Data.NIPSSDbContext _context;

        public LibraryModel(SEC44NIPSS.Data.NIPSSDbContext context)
        {
            _context = context;
        }

        public IList<DocumentCategory> DocumentCategory { get; set; }

        public async Task OnGetAsync()
        {
            DocumentCategory = await _context.DocumentCategories.Include(x => x.Documents).ToListAsync();


        }
    }
}
