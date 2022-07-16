using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SEC44NIPSS.Data.Model;

namespace SEC44NIPSS.Areas.Participant.Pages.Forms
{
    public class QuestionViewmodel
    {
        public OptionType OptionType { get; set; }
        public string ShortNote { get; set; }
        public string LongNote { get; set; }
        public string YesNo { get; set; }
        public string FourOption { get; set; }
        public string FiveOption { get; set; }
        public string MultipleOption { get; set; }



        public long QuestionId { get; set; }
    }
    public class PreviewModel : PageModel
    {
        private readonly SEC44NIPSS.Data.NIPSSDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IHostingEnvironment _hostingEnv;
        private int imgCount;

        public PreviewModel(SEC44NIPSS.Data.NIPSSDbContext context, UserManager<IdentityUser> userManager, IHostingEnvironment hostingEnv)
        {
            _context = context;
            _userManager = userManager;
            _hostingEnv = hostingEnv;
        }

        public Questionner QuestionnerList { get; set; }

        public async Task<IActionResult> OnGetAsync(long id)
        {

            QuestionnerList = await _context.Questionners.Include(x => x.Questions).ThenInclude(x => x.Options).FirstOrDefaultAsync(x => x.Id == id);
            if (QuestionnerList == null)

            {
                return RedirectToPage("./Index");
            }


            return Page();
        }

        //[BindProperty]
        //public QuestionViewmodel xmodel { get; set; }
        public async Task<IActionResult> OnPostAsync(IFormCollection form)
        {
            string xQuestionnnerId = form["QuestionnnerId"];
            long qId = Convert.ToInt64(xQuestionnnerId);

            long questionid = 0;
            string optionvalue = "";
            string Phone = "";
            string Email = "";

            Phone = form["Phone"];
            Email = form["Email"];
            var questions = await _context.Questions.Include(x=>x.Options).Where(x => x.QuestionnerId == qId).ToListAsync();
            QuestionAnswer ans = new QuestionAnswer();
            ans.Date = DateTime.UtcNow.AddHours(1);
            ans.Email = Email;
            ans.Phone = Phone;
            ans.QuestionnerId = qId;
            _context.QuestionAnswers.Add(ans);
            await _context.SaveChangesAsync();

            foreach (var x in questions)
            {
                string q = "Question-" + x.Id;
                string qid = form[q];

                questionid = Convert.ToInt64(qid);

                QuestionResponse qresponse = new QuestionResponse();
                qresponse.QuestionId = questionid;
                if (x.Options.OptionType == OptionType.FiveOption)
                {
                    string c = "FiveOption-"+x.Id;
                    optionvalue = form[c];
                }
                else if (x.Options.OptionType == OptionType.FourOption)
                {
                    string c = "FourOption-" + x.Id;
                    optionvalue = form[c];
                }
                else if (x.Options.OptionType == OptionType.MultipleOption)
                {
                    string c1 = "MultipleOption1-" + x.Id;
                    string c2 = "MultipleOption1-" + x.Id;
                    string c3 = "MultipleOption1-" + x.Id;
                    string c4 = "MultipleOption1-" + x.Id;
                    string c5 = "MultipleOption1-" + x.Id;
                    string c6 = "MultipleOption1-" + x.Id;

                    string allselected = "";
                    if (!String.IsNullOrEmpty(c1)) 
                    { allselected = form[c1]; }
                    if (!String.IsNullOrEmpty(c2))
                    { allselected = ", "+ form[c2]; }
                    if (!String.IsNullOrEmpty(c3))
                    { allselected = ", " + form[c3]; }
                    if (!String.IsNullOrEmpty(c4))
                    { allselected = ", " + form[c4]; }
                    if (!String.IsNullOrEmpty(c5))
                    { allselected = ", " + form[c5]; }
                    if (!String.IsNullOrEmpty(c6))
                    { allselected = ", " + form[c6]; }
                    optionvalue = allselected; 
                }
                else if (x.Options.OptionType == OptionType.YesNo)
                {
                    string c = "YesNo-" + x.Id;
                    optionvalue = form[c];
                }
                else if (x.Options.OptionType == OptionType.ShortNote)
                {
                    string c = "ShortNote-" + x.Id;
                    optionvalue = form[c];
                }
                else if (x.Options.OptionType == OptionType.ShortNote)
                {
                    string c = "LongNote-" + x.Id;
                    optionvalue = form[c];
                }

                qresponse.Answer = optionvalue;

                _context.QuestionResponses.Add(qresponse);
                

            }
            await _context.SaveChangesAsync();
            var qmain = await _context.Questionners.FirstOrDefaultAsync(x => x.Id == qId);
            return RedirectToPage("./Response", new { x = qmain.LongLink });
        }
    }
}
