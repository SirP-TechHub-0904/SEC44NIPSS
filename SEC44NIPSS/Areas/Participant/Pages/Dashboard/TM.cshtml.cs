using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SEC44NIPSS.Areas.Participant.Pages.Dashboard
{
    public class TMModel : PageModel
    {
        public async Task<IActionResult> OnGetAsync(string date = null, string searchdate = null, string searchdatecancel = null, string id = null, string x = null)
        {
            //if (id != null)
            //{
            //    string ddate = id.Insert(2, "/");
            //    string ddatex = ddate.Insert(6, "/");
               
            //        ddatex = ddatex.Replace("single", "");
            //    TempData["date"] = ddatex;
            //}
            
            //return Page();
            return RedirectToPage("/Dashboard/Timetable", new { area = "Participant", id = id });
        }
    }
}
