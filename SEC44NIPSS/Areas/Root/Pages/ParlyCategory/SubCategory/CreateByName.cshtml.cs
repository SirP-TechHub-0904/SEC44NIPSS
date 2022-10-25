using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SEC44NIPSS.Data;
using SEC44NIPSS.Data.Model;

namespace SEC44NIPSS.Areas.Root.Pages.ParlyCategory.SubCategory
{
    public class CreateByNameModel : PageModel
    {
        private readonly SEC44NIPSS.Data.NIPSSDbContext _context;

        public CreateByNameModel(SEC44NIPSS.Data.NIPSSDbContext context)
        {
            _context = context;
        }
        public long Id { get; set; }
        public ParlyReportCategory ParlyReportCategory { get; set; }

        public async Task<IActionResult> OnGet()
        {
            //ParlyReportCategory = await _context.ParlyReportCategories.FirstOrDefaultAsync(x => x.Id == id);
            //if(ParlyReportCategory == null)
            //{
            //    return RedirectToPage("/ParlyCategory/Index");
            //}
            var secc = await _context.Participants.Include(x=>x.Profile).Where(p => p.IsTrue == true).OrderBy(x=>x.Profile.Title).ToListAsync();
            int sn = 0;
            var cat = await _context.ParlyReportCategories.FirstOrDefaultAsync(x => x.Id == 56);
            foreach(var x in secc)
            {
               

                Random r = new Random();

                string[] words = { 
                    "bg-red",
                "bg-yellow",
                "bg-aqua",
                "bg-blue",
                "bg-light-blue",
                "bg-green",
                "bg-navy",
                "bg-teal",
                "bg-olive",
                "bg-lime",
                "bg-orange",
                "bg-fuchsia",
                "bg-purple",
                "bg-maroon",
                "bg-red-active",
                "bg-yellow-active",
                "bg-aqua-active",
                "bg-blue-active",
                
                };

               string word = words[r.Next(0, words.Length)];
                ParlyReportSubCategory p = new ParlyReportSubCategory();
                p.BgColor = word;
                p.Title = x.Profile.Title + " " + x.Profile.FullName;
                p.SortOrder = sn++;
                p.Description = "";
                p.ParlyReportCategoryId = cat.Id;
                p.Date = DateTime.UtcNow;

                //_context.ParlyReportSubCategories.Add(p);
               
                int dof = 0;
            }
          //  await _context.SaveChangesAsync();
            return Page();
        }

        [BindProperty]
        public ParlyReportSubCategory ParlyReportSubCategory { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            //_context.ParlyReportSubCategories.Add(ParlyReportSubCategory);
            //await _context.SaveChangesAsync();

            return RedirectToPage("./Folders", new { id = ParlyReportSubCategory.ParlyReportCategoryId });
        }
    }
}
