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

namespace SEC44NIPSS.Areas.Participant.Pages.ProjectPage
{
    [Authorize]
        public class ResultModel : PageModel
    {
        private readonly SEC44NIPSS.Data.NIPSSDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public ResultModel(SEC44NIPSS.Data.NIPSSDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public LegacyProject LegacyProject { get; set; }

        public IList<LegacyProjectAnswer> LegacyProjectAnswers { get; set; }
        [BindProperty]
        public string Email { get; set; }
        public int P1 { get; set; }
        public int P2 { get; set; }
        public int P3 { get; set; }
        public int P4 { get; set; }
        public int P5 { get; set; }
        public int P6 { get; set; }
        public int P7 { get; set; }
        public int P8 { get; set; }
        public int P9 { get; set; }
        public int P10 { get; set; }

        public decimal xP1 { get; set; }
        public decimal xP2 { get; set; }
        public decimal xP3 { get; set; }
        public decimal xP4 { get; set; }
        public decimal xP5 { get; set; }
        public decimal xP6 { get; set; }
        public decimal xP7 { get; set; }
        public decimal xP8 { get; set; }
        public decimal xP9 { get; set; }
        public decimal xP10 { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            
            LegacyProjectAnswers = await _context.LegacyProjectAnswers.Where(x=>x.VotingType == VotingType.Project).ToListAsync();
            LegacyProject = await _context.LegacyProjects.FirstOrDefaultAsync();

            P1 = LegacyProjectAnswers.Where(x => x.Answer == "P1").Count();
            P2 = LegacyProjectAnswers.Where(x => x.Answer == "P2").Count();
            P3 = LegacyProjectAnswers.Where(x => x.Answer == "P3").Count();
            P4 = LegacyProjectAnswers.Where(x => x.Answer == "P4").Count();
            P5 = LegacyProjectAnswers.Where(x => x.Answer == "P5").Count();
            P6 = LegacyProjectAnswers.Where(x => x.Answer == "P6").Count();
            P7 = LegacyProjectAnswers.Where(x => x.Answer == "P7").Count();
            P8 = LegacyProjectAnswers.Where(x => x.Answer == "P8").Count();
            P9 = LegacyProjectAnswers.Where(x => x.Answer == "P9").Count();
            P10 = LegacyProjectAnswers.Where(x => x.Answer == "P10").Count();

            var xLegacyProjectAnswers = await _context.LegacyProjectAnswers.Where(x => x.VotingType == VotingType.Project).ToListAsync();
            decimal p1 = xLegacyProjectAnswers.Where(x => x.Answer == "P1").Count();
            decimal p2 = xLegacyProjectAnswers.Where(x => x.Answer == "P2").Count();
            decimal p3 = xLegacyProjectAnswers.Where(x => x.Answer == "P3").Count();
            decimal p4 = xLegacyProjectAnswers.Where(x => x.Answer == "P4").Count();
            decimal p5 = xLegacyProjectAnswers.Where(x => x.Answer == "P5").Count();
            decimal p6 = xLegacyProjectAnswers.Where(x => x.Answer == "P6").Count();
            decimal p7 = xLegacyProjectAnswers.Where(x => x.Answer == "P7").Count();
            decimal p8 = xLegacyProjectAnswers.Where(x => x.Answer == "P8").Count();
            decimal p9 = xLegacyProjectAnswers.Where(x => x.Answer == "P9").Count();
            decimal p10 = xLegacyProjectAnswers.Where(x => x.Answer == "P10").Count();


            decimal ols = Convert.ToDecimal((p1 / Convert.ToDecimal(90)) * Convert.ToDecimal(100));
            xP1 = Convert.ToDecimal((p1 / Convert.ToDecimal(90)) * Convert.ToDecimal(100));
            xP2 = Convert.ToDecimal((p2 / Convert.ToDecimal(90)) * Convert.ToDecimal(100));
            xP3 = Convert.ToDecimal((p3 / Convert.ToDecimal(90)) * Convert.ToDecimal(100));
            xP4 = Convert.ToDecimal((p4 / Convert.ToDecimal(90)) * Convert.ToDecimal(100));
            xP5 = Convert.ToDecimal((p5 / Convert.ToDecimal(90)) * Convert.ToDecimal(100));
            xP6 = Convert.ToDecimal((p6 / Convert.ToDecimal(90)) * Convert.ToDecimal(100));
            xP7 = Convert.ToDecimal((p7 / Convert.ToDecimal(90)) * Convert.ToDecimal(100));
            xP8 = Convert.ToDecimal((p8 / Convert.ToDecimal(90)) * Convert.ToDecimal(100));
            xP9 = Convert.ToDecimal((p9 / Convert.ToDecimal(90)) * Convert.ToDecimal(100));
            xP10 = Convert.ToDecimal((p10 / Convert.ToDecimal(90)) * Convert.ToDecimal(100));



            return Page();
        }
       
    }

}
