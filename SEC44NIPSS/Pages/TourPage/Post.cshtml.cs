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
    public class PostModel : PageModel
    {
        private readonly SEC44NIPSS.Data.NIPSSDbContext _context;

        public PostModel(SEC44NIPSS.Data.NIPSSDbContext context)
        {
            _context = context;
        }

        public IList<TourPost> TourPost { get;set; }
        public IList<TourSubCategory> TourSubCategories { get; set; }
        public string Title { get;set; }

        public async Task OnGetAsync(long id)
        {
            TourPost = await _context.TourPosts
                .Include(t => t.TourPostType)
                .Include(t => t.TourSubCategory).Where(x => x.TourSubCategoryId == id).ToListAsync();
            var title = await _context.TourSubCategories.FirstOrDefaultAsync(x => x.Id == id);
            Title = title.Title;

            TourSubCategories = await _context.TourSubCategories.ToListAsync();
        }
    }
}
