using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SEC44NIPSS.Data.Model;

namespace SEC44NIPSS.Areas.Participant.Pages.Forms
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly SEC44NIPSS.Data.NIPSSDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public IndexModel(SEC44NIPSS.Data.NIPSSDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IList<Questionner> QuestionnerList { get; set; }

        public async Task OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            var profile = await _context.Profiles.FirstOrDefaultAsync(x => x.UserId == user.Id);
            ProfileId = profile.Id;
            QuestionnerList = await _context.Questionners.Where(x => x.ProfileId == profile.Id).OrderByDescending(x => x.Date).ToListAsync();
        }
        [BindProperty]
        public Questionner Questionner { get; set; }

        [BindProperty]
        public long ProfileId { get; set; }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Questionner.ShortLink = CreateD();
            string code = Guid.NewGuid().ToString();
            string code1 = Guid.NewGuid().ToString();
            string code2 = Guid.NewGuid().ToString();
            Questionner.LongLink = code + CreateD() + code1 + code2;
            Questionner.ProfileId = ProfileId;
            Questionner.Date = DateTime.UtcNow.AddHours(1);
            Questionner.Title = DateTime.UtcNow.AddHours(1).ToString("dd MMM") + " QUESTIONNER";
            Questionner.Description = "These a Questionner in your area of study";
            Questionner.Response = "<i>THANK YOU.</i> We have received your feedback.";
            Questionner.Instruction = "Attempt all question to the best of your knowledge";
            Questionner.Email = EmailPhoneStatus.No;
            Questionner.PhoneNumber = EmailPhoneStatus.No;
            _context.Questionners.Add(Questionner);
            await _context.SaveChangesAsync();

            Question q = new Question();
            q.QuestionnerId = Questionner.Id;
            q.Title = "What type of Questionner do you want to create";
            q.Number = 1;
            q.SortOrder = 1;
            _context.Questions.Add(q);
            await _context.SaveChangesAsync();

            Option o = new Option();
            o.QuestionId = q.Id;
            o.OptionType = OptionType.FourOption;
            o.OptionList1 = "Interview";
            o.OptionList2 = "Examination";
            o.OptionList3 = "Marketing";
            o.OptionList4 = "Sign Up";

            _context.Options.Add(o);
            await _context.SaveChangesAsync();


            return RedirectToPage("./FormMaker", new { o = Questionner.ShortLink, q = Questionner.LongLink });
        }


        private static string CreateA(int length = 3)
        {
            // Create a string of characters, numbers, special characters that allowed in the password  
            string validChars = "ABCDEFGHJKLMNOPQRSTUVWXYZ";
            Random random = new Random();

            // Select one random character at a time from the string  
            // and create an array of chars  
            char[] chars = new char[length];
            for (int i = 0; i < length; i++)
            {
                chars[i] = validChars[random.Next(0, validChars.Length)];
            }
            return new string(chars);
        }
        private static string CreateB(int length = 3)
        {
            // Create a string of characters, numbers, special characters that allowed in the password  
            string validChars = "abcdefghijklmnopqrstuvwxyz";
            Random random = new Random();

            // Select one random character at a time from the string  
            // and create an array of chars  
            char[] chars = new char[length];
            for (int i = 0; i < length; i++)
            {
                chars[i] = validChars[random.Next(0, validChars.Length)];
            }
            return new string(chars);
        }
        private static string CreateC(int length = 2)
        {
            // Create a string of characters, numbers, special characters that allowed in the password  
            string validChars = "0123456789";
            Random random = new Random();

            // Select one random character at a time from the string  
            // and create an array of chars  
            char[] chars = new char[length];
            for (int i = 0; i < length; i++)
            {
                chars[i] = validChars[random.Next(0, validChars.Length)];
            }
            return new string(chars);
        }

        private static string CreateD(int length = 8)
        {
            // Create a string of characters, numbers, special characters that allowed in the password  
            string validChars = CreateA() + CreateB() + CreateC();
            Random random = new Random();

            // Select one random character at a time from the string  
            // and create an array of chars  
            char[] chars = new char[length];
            for (int i = 0; i < length; i++)
            {
                chars[i] = validChars[random.Next(0, validChars.Length)];
            }
            return new string(chars);
        }



      


    }
}
