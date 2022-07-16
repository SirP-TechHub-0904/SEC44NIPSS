using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SEC44NIPSS.Data.Model;

namespace SEC44NIPSS.Areas.Participant.Pages.ProjectPage
{
    //[Authorize]
        public class ResultContributionModel : PageModel
    {
        private readonly SEC44NIPSS.Data.NIPSSDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public ResultContributionModel(SEC44NIPSS.Data.NIPSSDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public LegacyProject LegacyProject { get; set; }

        public IList<LegacyProjectAnswer> LegacyProjectAnswers { get; set; }
        [BindProperty]
        public string Email { get; set; }
        public int C1 { get; set; }
        public int C2 { get; set; }
        public int C3 { get; set; }
        public int C4 { get; set; }
       


        public decimal xC1 { get; set; }
        public decimal xC2 { get; set; }
        public decimal xC3 { get; set; }
        public decimal xC4 { get; set; }
      


        public async Task<IActionResult> OnGetAsync()
        {
            
            LegacyProjectAnswers = await _context.LegacyProjectAnswers.Where(x=>x.VotingType == VotingType.Contribution).ToListAsync();
            LegacyProject = await _context.LegacyProjects.FirstOrDefaultAsync();

            C1 = LegacyProjectAnswers.Where(x => x.Answer == "C1").Count();
            C2 = LegacyProjectAnswers.Where(x => x.Answer == "C2").Count();
            C3 = LegacyProjectAnswers.Where(x => x.Answer == "C3").Count();
            C4 = LegacyProjectAnswers.Where(x => x.Answer == "C4").Count();
          

            var xLegacyProjectAnswers = await _context.LegacyProjectAnswers.Where(x => x.VotingType == VotingType.Contribution).ToListAsync();
            decimal c1 = xLegacyProjectAnswers.Where(x => x.Answer == "C1").Count();
            decimal c2 = xLegacyProjectAnswers.Where(x => x.Answer == "C2").Count();
            decimal c3 = xLegacyProjectAnswers.Where(x => x.Answer == "C3").Count();
            decimal c4 = xLegacyProjectAnswers.Where(x => x.Answer == "C4").Count();
           



            decimal ols = Convert.ToDecimal((c1 / Convert.ToDecimal(90)) * Convert.ToDecimal(100));
            xC1 = Convert.ToDecimal((c1 / Convert.ToDecimal(90)) * Convert.ToDecimal(100));
            xC2 = Convert.ToDecimal((c2 / Convert.ToDecimal(90)) * Convert.ToDecimal(100));
            xC3 = Convert.ToDecimal((c3 / Convert.ToDecimal(90)) * Convert.ToDecimal(100));
            xC4 = Convert.ToDecimal((c4 / Convert.ToDecimal(90)) * Convert.ToDecimal(100));

            var callbackUrl = Url.Page(
                         "/ProjectPage/ResultContribution",
                         pageHandler: null,
                         values: new { area = "Participant" },
                         protocol: Request.Scheme);

            string mi = $"{HtmlEncoder.Default.Encode(callbackUrl)}";
            TempData["link"] = mi;

            return Page();
        }
       
    }

}
