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
    public class QuestionnerModel : PageModel
    {
        private readonly SEC44NIPSS.Data.NIPSSDbContext _context;

        public QuestionnerModel(SEC44NIPSS.Data.NIPSSDbContext context)
        {
            _context = context;
        }

        public Questionner Questionner { get; set; }

        [BindProperty]
        public string Email { get; set; }

        [BindProperty]
        public string Phone { get; set; }
        [BindProperty]
        public long QuestionnerId { get; set; }

        public async Task<IActionResult> OnGetAsync(string name)
        {
            Questionner = await _context.Questionners
                .Include(x => x.Questions).FirstOrDefaultAsync(x => x.ShortLink == name);

          
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            Answer a = new Answer();
            a.Email = Email;
            a.Phone = Phone;
            a.QuestionnerId = QuestionnerId;
            _context.Answers.Add(a);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Question", new { id = QuestionnerId });
        }
    }
}
