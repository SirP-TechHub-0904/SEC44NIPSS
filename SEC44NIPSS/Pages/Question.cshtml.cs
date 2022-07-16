using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SEC44NIPSS.Data.Model;

namespace SEC44NIPSS.Pages
{
    public class QuestionModel : PageModel
    {
        private readonly SEC44NIPSS.Data.NIPSSDbContext _context;

        public QuestionModel(SEC44NIPSS.Data.NIPSSDbContext context)
        {
            _context = context;
        }

        public IList<Question> Question { get; set; }
        public Questionner Questionner { get; set; }

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Question = await _context.Questions
                .Include(q => q.Questionner)
                .Include(q => q.Options)
                .Where(x => x.QuestionnerId == id).ToListAsync();

            Questionner = await _context.Questionners.FirstOrDefaultAsync(x => x.Id == id);
            return Page();
        }


    }
}
