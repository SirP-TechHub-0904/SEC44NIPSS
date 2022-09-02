using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SEC44NIPSS.Data;
using SEC44NIPSS.Data.Model;

namespace SEC44NIPSS.Areas.Admin.Pages.Rapid.Answers
{
    public class IndexModel : PageModel
    {
        private readonly SEC44NIPSS.Data.NIPSSDbContext _context;

        public IndexModel(SEC44NIPSS.Data.NIPSSDbContext context)
        {
            _context = context;
        }

        public IList<UserAnswer> UserAnswer { get; set; }

        public async Task OnGetAsync()
        {
            UserAnswer = await _context.UserAnswers
                .Include(u => u.Profile).OrderByDescending(x => x.Id).ToListAsync();

            //var pUserAnswer = await _context.UserAnswers.Where(x => x.StartTime.Value.AddMinutes(4) < DateTime.UtcNow.AddHours(1) && x.StartTime != null).ToListAsync();

            //foreach (var x in pUserAnswer)
            //{

            //    var userlist = await _context.AnswerLists.AsNoTracking().Include(x => x.RapidQuestion).ThenInclude(x => x.RapidOptions).Include(x => x.UserAnswer).Where(x => x.UserAnswerId == x.UserAnswerId).OrderBy(x => x.SN).ToListAsync();



            //    var xgood = userlist.Where(x => x.Choose == x.Answer && x.Choose != null).ToList();
            //    var xbad = userlist.Where(x => x.Choose != x.Answer && x.Choose != null).ToList();

            //    int good = userlist.Where(x => x.Choose == x.Answer && x.Choose != null).Count();
            //    int bad = userlist.Where(x => x.Choose != x.Answer && x.Choose != null).Count();
            //    x.Score = "";
            //    x.Score = (good * 5).ToString() + "%";
            //    _context.Attach(x).State = EntityState.Modified;

            //}
            await _context.SaveChangesAsync();
        }

        public async Task<IActionResult> OnPostReset()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

          


            var xUserAnswer = await _context.UserAnswers
                .Include(u => u.Profile).OrderByDescending(x => x.Id).Where(x=>x.QuestionsLoaded == false).ToListAsync();
            foreach (var x in xUserAnswer)
            {
                x.StartTime = null;
                
                _context.Attach(x).State = EntityState.Modified;
            }
            await _context.SaveChangesAsync();


            return RedirectToPage("./Index");
        }

    }
}
