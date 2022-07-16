using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SEC44NIPSS.Data;
using SEC44NIPSS.Data.Model;

namespace SEC44NIPSS.Pages.TourPage
{
    public class NipssTourModel : PageModel
    {
        private readonly SEC44NIPSS.Data.NIPSSDbContext _context;

        public NipssTourModel(SEC44NIPSS.Data.NIPSSDbContext context)
        {
            _context = context;
        }

        public IList<TourCategory> TourCategory { get;set; }
        public IList<StudyGroup> StudyGroup { get;set; }

        public async Task OnGetAsync()
        {
            TourCategory = await _context.TourCategories.Include(x=>x.TourSubCategories).ToListAsync();
            StudyGroup = await _context.StudyGroups.ToListAsync();


        }
    }
}
