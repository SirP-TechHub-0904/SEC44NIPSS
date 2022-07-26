using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SEC44NIPSS.Data;
using SEC44NIPSS.Data.Model;

namespace SEC44NIPSS.Areas.Participant.Pages.Forms
{
    public class UpdateQuestionPageModel : PageModel
    {
        private readonly SEC44NIPSS.Data.NIPSSDbContext _context;

        public UpdateQuestionPageModel(SEC44NIPSS.Data.NIPSSDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public QuestionnerPage QuestionnerPage { get; set; }
        [BindProperty]
        public string siq { get; set; }

        [BindProperty]
        public string liq { get; set; }
        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            QuestionnerPage = await _context.QuestionnerPages
                .Include(q => q.Questionner).FirstOrDefaultAsync(m => m.Id == id);

            if (QuestionnerPage == null)
            {
                return NotFound();
            }
            siq = QuestionnerPage.Questionner.ShortLink;
            liq = QuestionnerPage.Questionner.LongLink;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(QuestionnerPage).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!QuestionnerPageExists(QuestionnerPage.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            var qx = await _context.Questionners.Include(x => x.QuestionnerPages).FirstOrDefaultAsync(x => x.Id == QuestionnerPage.QuestionnerId);

            return RedirectToPage("./FormMaker", new { o = qx.ShortLink, q = qx.LongLink });
        }

        private bool QuestionnerPageExists(long id)
        {
            return _context.QuestionnerPages.Any(e => e.Id == id);
        }
    }
}
