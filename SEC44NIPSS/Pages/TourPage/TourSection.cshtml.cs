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
    public class TourSectionModel : PageModel
    {
        private readonly SEC44NIPSS.Data.NIPSSDbContext _context;

        public TourSectionModel(SEC44NIPSS.Data.NIPSSDbContext context)
        {
            _context = context;
        }

        public IList<TourSubCategory> TourSubCategory { get;set; }
        public TourCategory TourCategory { get;set; }

        public async Task OnGetAsync(long id)
        {
            TourSubCategory = await _context.TourSubCategories
                .Include(t => t.StudyGroup)
                .Include(t => t.TourCategory).Where(x=>x.TourCategoryId == id).ToListAsync();

            TourCategory = await _context.TourCategories.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
