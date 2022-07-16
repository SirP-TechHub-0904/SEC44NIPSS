using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SEC44NIPSS.Data;
using SEC44NIPSS.Data.Model;

namespace SEC44NIPSS.Areas.Participant.Pages.Dashboard
{
    [Microsoft.AspNetCore.Authorization.Authorize]

    public class ResultModel : PageModel
    {
        private readonly SEC44NIPSS.Data.NIPSSDbContext _context;

        public ResultModel(SEC44NIPSS.Data.NIPSSDbContext context)
        {
            _context = context;
        }

        public UserAnswer UserAnswer { get; set; }

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            UserAnswer = await _context.UserAnswers
                .Include(u => u.Profile).FirstOrDefaultAsync(m => m.Id == id);

            var userlist = await _context.AnswerLists.Where(x => x.UserAnswerId == id).ToListAsync();
            int good = userlist.Where(x => x.Choose == x.Answer).Count();

            UserAnswer.Score = (good * 5).ToString()+"%";
            _context.Attach(UserAnswer).State = EntityState.Modified;

            
                await _context.SaveChangesAsync();

                if (UserAnswer == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
