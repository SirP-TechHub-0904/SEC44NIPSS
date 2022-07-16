using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SEC44NIPSS.Areas.Participant.Pages.Dashboard
{
    [Authorize]
    public class VotingModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
