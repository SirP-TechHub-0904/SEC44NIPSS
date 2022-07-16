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
    [Authorize]
    public class ResultDueModel : PageModel
    {
        private readonly SEC44NIPSS.Data.NIPSSDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public ResultDueModel(SEC44NIPSS.Data.NIPSSDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public LegacyProject LegacyProject { get; set; }

        public IList<LegacyProjectAnswer> LegacyProjectAnswers { get; set; }
        [BindProperty]
        public string Email { get; set; }
        public int D1 { get; set; }
        public int D2 { get; set; }
        public int D3 { get; set; }
        public int D4 { get; set; }



        public decimal xD1 { get; set; }
        public decimal xD2 { get; set; }
        public decimal xD3 { get; set; }
        public decimal xD4 { get; set; }



        public async Task<IActionResult> OnGetAsync()
        {

            LegacyProjectAnswers = await _context.LegacyProjectAnswers.Where(x => x.VotingType == VotingType.Dues).ToListAsync();
            LegacyProject = await _context.LegacyProjects.FirstOrDefaultAsync();

            D1 = LegacyProjectAnswers.Where(x => x.Answer == "D1").Count();
            D2 = LegacyProjectAnswers.Where(x => x.Answer == "D2").Count();
            D3 = LegacyProjectAnswers.Where(x => x.Answer == "D3").Count();
            D4 = LegacyProjectAnswers.Where(x => x.Answer == "D4").Count();


            var xLegacyProjectAnswers = await _context.LegacyProjectAnswers.Where(x => x.VotingType == VotingType.Dues).ToListAsync();
            decimal d1 = xLegacyProjectAnswers.Where(x => x.Answer == "D1").Count();
            decimal d2 = xLegacyProjectAnswers.Where(x => x.Answer == "D2").Count();
            decimal d3 = xLegacyProjectAnswers.Where(x => x.Answer == "D3").Count();
            decimal d4 = xLegacyProjectAnswers.Where(x => x.Answer == "D4").Count();




            decimal ols = Convert.ToDecimal((d1 / Convert.ToDecimal(90)) * Convert.ToDecimal(100));
            xD1 = Convert.ToDecimal((d1 / Convert.ToDecimal(90)) * Convert.ToDecimal(100));
            xD2 = Convert.ToDecimal((d2 / Convert.ToDecimal(90)) * Convert.ToDecimal(100));
            xD3 = Convert.ToDecimal((d3 / Convert.ToDecimal(90)) * Convert.ToDecimal(100));
            xD4 = Convert.ToDecimal((d4 / Convert.ToDecimal(90)) * Convert.ToDecimal(100));

            var callbackUrl = Url.Page(
                         "/ProjectPage/ResultDue",
                         pageHandler: null,
                         values: new { area = "Participant" },
                         protocol: Request.Scheme);

            string mi = $"{HtmlEncoder.Default.Encode(callbackUrl)}";
            TempData["link"] = mi;
            return Page();
        }

    }

}
