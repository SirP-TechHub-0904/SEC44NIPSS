using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SEC44NIPSS.Data;
using SEC44NIPSS.Data.Model;

namespace SEC44NIPSS.Areas.Admin.Pages.Rapid
{
    public class DetailsModel : PageModel
    {
        private readonly SEC44NIPSS.Data.NIPSSDbContext _context;

        public DetailsModel(SEC44NIPSS.Data.NIPSSDbContext context)
        {
            _context = context;
        }
        [BindProperty]
        public RapidQuestion RapidQuestion { get; set; }

        [BindProperty]
        public RapidOption RapidOption { get; set; }
        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            RapidQuestion = await _context.RapidQuestions.Include(x=>x.RapidOptions).FirstOrDefaultAsync(m => m.Id == id);
            RapidOption = await _context.RapidOption.FirstOrDefaultAsync(m => m.RapidQuestionId == id);

            if (RapidQuestion == null)
            {
                return NotFound();
            }
            return Page();
        }

       
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

           
            _context.Attach(RapidQuestion).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            _context.Attach(RapidOption).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return RedirectToPage("./Details", new { id = RapidOption.RapidQuestionId });
        }
    }
}
