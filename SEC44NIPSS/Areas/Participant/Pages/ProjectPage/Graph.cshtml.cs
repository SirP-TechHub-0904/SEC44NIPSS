using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SEC44NIPSS.Data.Model;
using SEC44NIPSS.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SEC44NIPSS.Areas.Participant.Pages.ProjectPage
{
    public class GraphModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly SEC44NIPSS.Data.NIPSSDbContext _context;
        private readonly INotificationService _notificationService;
        private readonly UserManager<IdentityUser> _userManager;


        public GraphModel(ILogger<IndexModel> logger, Data.NIPSSDbContext context, INotificationService notificationService, UserManager<IdentityUser> userManager)
        {
            _logger = logger;
            _context = context;
            _notificationService = notificationService;
            _userManager = userManager;
        }
        public IList<Executive> Executive { get; set; }
        public IList<Profile> NipssStaffList { get; set; }
        public IList<News> News { get; set; }
        public IList<CurrentAffair> CurrentAffair { get; set; }

        public async Task OnGetAsync()
        {
         
            var LegacyProjectAnswers = await _context.LegacyProjectAnswers.ToListAsync();
            decimal p1 = LegacyProjectAnswers.Where(x => x.Answer == "P1").Count();
            decimal p2 = LegacyProjectAnswers.Where(x => x.Answer == "P2").Count();
            decimal p3 = LegacyProjectAnswers.Where(x => x.Answer == "P3").Count();
            decimal p4 = LegacyProjectAnswers.Where(x => x.Answer == "P4").Count();
            decimal p5 = LegacyProjectAnswers.Where(x => x.Answer == "P5").Count();
            decimal p6 = LegacyProjectAnswers.Where(x => x.Answer == "P6").Count();
            decimal p7 = LegacyProjectAnswers.Where(x => x.Answer == "P7").Count();
            decimal p8 = LegacyProjectAnswers.Where(x => x.Answer == "P8").Count();
            decimal p9 = LegacyProjectAnswers.Where(x => x.Answer == "P9").Count();
            decimal p10 = LegacyProjectAnswers.Where(x => x.Answer == "P10").Count();


            int ols = Convert.ToInt32((p1 / 90) * 100);
            P1 = Convert.ToInt32((p1 / 90) * 100);
            P2 = Convert.ToInt32((p2 / 90) * 100);
            P3 = Convert.ToInt32((p3 / 90) * 100);
            P4 = Convert.ToInt32((p4 / 90) * 100);
            P5 = Convert.ToInt32((p5 / 90) * 100);
            P6 = Convert.ToInt32((p6 / 90) * 100);
            P7 = Convert.ToInt32((p7 / 90) * 100);
            P8 = Convert.ToInt32((p8 / 90) * 100);
            P9 = Convert.ToInt32((p9 / 90) * 100);
            P10 = Convert.ToInt32((p10 / 90) * 100);



        }

       

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
     

    }
}
