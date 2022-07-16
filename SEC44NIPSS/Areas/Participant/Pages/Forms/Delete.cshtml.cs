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
    public class DeleteModel : PageModel
    {
        private readonly SEC44NIPSS.Data.NIPSSDbContext _context;

        public DeleteModel(SEC44NIPSS.Data.NIPSSDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Questionner Questionner { get; set; }

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Questionner = await _context.Questionners
                .Include(q => q.Profile).FirstOrDefaultAsync(m => m.Id == id);

            if (Questionner == null)
            {
                return NotFound();
            }
            return Page();
        }
        [BindProperty]
        public string Title { get; set; }

        public async Task<IActionResult> OnPostAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Questionner = await _context.Questionners.FindAsync(id);

            if (Questionner != null)
            {
                if (Questionner.Title == Title)
                {
                    var question = await _context.Questions.Where(x => x.QuestionnerId == Questionner.Id).ToListAsync();
                    foreach (var d in question)
                    {
                        var opt = await _context.Options.Where(x => x.QuestionId == d.Id).ToListAsync();
                        foreach (var y in opt)
                        {
                            _context.Options.Remove(y);
                            await _context.SaveChangesAsync();
                        }
                        _context.Questions.Remove(d);
                        await _context.SaveChangesAsync();
                    }

                    _context.Questionners.Remove(Questionner);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    TempData["error"] = "unable to delete questionner";
                    return Page();
                }
            }
            else
            {
                TempData["error"] = "unable to delete questionner";
                return RedirectToPage("./Index");
            }

            return RedirectToPage("./Index");
        }
    }
}
