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
    public class StartTestModel : PageModel
    {
        private readonly SEC44NIPSS.Data.NIPSSDbContext _context;

        public StartTestModel(SEC44NIPSS.Data.NIPSSDbContext context)
        {
            _context = context;
        }
        [BindProperty]
        public UserAnswer UserAnswer { get; set; }
        [BindProperty]
        public RapidQuestion RapidQuestion { get; set; }
        [BindProperty]
        public AnswerList AnswerList { get; set; }
        [BindProperty]
        public long? Qid { get; set; }
        public async Task<IActionResult> OnGetAsync(long? id, int qid = 0)
        {
            if (id == null)
            {
                return NotFound();
            }

            UserAnswer = await _context.UserAnswers
                .Include(u => u.Profile).FirstOrDefaultAsync(m => m.Id == id);
            if (UserAnswer.StartTime != null)
            {
                if (UserAnswer.StartTime.Value.AddMinutes(4) < DateTime.UtcNow.AddHours(1))
                {
                    return RedirectToPage("Result", new { id = UserAnswer.Id });
                }
            }
            Qid = id;
            if (UserAnswer.QuestionsLoaded == false)
            {
                var questions = await _context.RapidQuestions.OrderBy(x => Guid.NewGuid()).Take(20).ToListAsync();
                int sn = 1;
                foreach (var x in questions)
                {
                    AnswerList list = new AnswerList();
                    list.RapidQuestionId = x.Id;
                    list.UserAnswerId = id ?? 0;
                    list.SN = sn++;
                    _context.AnswerLists.Add(list);
                }
                UserAnswer.QuestionsLoaded = true;
                UserAnswer.QuestionNumber = sn-1;
               
                UserAnswer.StartTime = DateTime.UtcNow.AddHours(1);
                _context.Attach(UserAnswer).State = EntityState.Modified;

                await _context.SaveChangesAsync();
            }
            else
            {
                UserAnswer.StartTime = DateTime.UtcNow.AddHours(1);
                _context.Attach(UserAnswer).State = EntityState.Modified;

                await _context.SaveChangesAsync();
            }
            if (qid == 0)
            {
                AnswerList = await _context.AnswerLists.Include(x=>x.RapidQuestion).Include(x=>x.RapidQuestion.RapidOptions).FirstOrDefaultAsync(x => x.SN == 1 && x.UserAnswerId== id);
            }
            else
            {
                AnswerList = await _context.AnswerLists.Include(x => x.RapidQuestion).Include(x => x.RapidQuestion.RapidOptions).FirstOrDefaultAsync(x => x.SN == qid && x.UserAnswerId == id);
            }
            return Page();
        }

        //optionx ioptionx: $('#optionx').val(),
        //iidx: $('#idx').val()
        public async Task<JsonResult> OnGetResultt(string ioptionx, long iidx, long uiidx)
        {
            try
            {
                var qx = await _context.AnswerLists.Include(x => x.RapidQuestion).Include(x => x.UserAnswer).Include(x=>x.RapidQuestion.RapidOptions).FirstOrDefaultAsync(x => x.SN == iidx && x.UserAnswerId == uiidx);
                if (qx.UserAnswer.StartTime.Value.AddMinutes(4) < DateTime.UtcNow.AddHours(1))
                {
                    return new JsonResult("Result"+ qx.UserAnswerId);
                }

                if (ioptionx == qx.RapidQuestion.RapidOptions.Answer)
                {
                    qx.Choose = ioptionx;
                    qx.Answer = qx.RapidQuestion.RapidOptions.Answer;
                    
                    _context.Attach(qx).State = EntityState.Modified;

                        await _context.SaveChangesAsync();
                        return new JsonResult("true");
                }
                else
                {
                    qx.Choose = ioptionx;
                    qx.Answer = qx.RapidQuestion.RapidOptions.Answer;
                    _context.Attach(qx).State = EntityState.Modified;

                    await _context.SaveChangesAsync();
                    return new JsonResult("false "+ qx.RapidQuestion.RapidOptions.Answer);
                }

              
                
            }
            catch (Exception k)
            {
                return new JsonResult("not found");
            }
        }

    }
}
