using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SEC44NIPSS.Data;
using SEC44NIPSS.Data.Model;

namespace SEC44NIPSS.Areas.NIPSS.Pages.Menu
{
    [Authorize]

    public class TimeTableModel : PageModel
    {
        private readonly SEC44NIPSS.Data.NIPSSDbContext _context;

        public TimeTableModel(SEC44NIPSS.Data.NIPSSDbContext context)
        {
            _context = context;
        }
        public IQueryable<Event> Event { get; set; }
        public IQueryable<string> Events { get; set; }
        public string Desc { get; set; }
        public string PreviousWeek { get; set; }
        public string PreviousWeekTitle { get; set; }
        public string NextWeek { get; set; }
        public string NextWeekTitle { get; set; }
        public string Title { get; set; }

        public async Task OnGetAsync(string date = null, string searchdate = null, string searchdatecancel = null, string id = null, string x = null)
        {
            //name="searchdatecancel" value="cancel"
            IQueryable<Event> evct = from s in _context.Events.OrderByDescending(x => x.Date)
                                         //.Where(x=>x.Date.DayOfWeek == DateTime.UtcNow.DayOfWeek)
                                     select s;
            if (searchdatecancel == "cancel")
            {
                searchdate = null;
            }
            DateTime givenDate = DateTime.Today;

            if (id != null)
            {
                string ddate = id.Insert(2, "/");
                string ddatex = ddate.Insert(6, "/");
                if (ddatex.Contains("single"))
                {
                    ddatex = ddatex.Replace("single", "");
                    date = null;
                    searchdate = ddatex;
                }
                else
                {
                    //week
                    date = ddatex;
                    searchdate = null;
                }
            }
            if (date != null)
            {

                givenDate = DateTime.Parse(date).AddDays(1);
            }

            if (searchdate != null)
            {
                givenDate = DateTime.Parse(searchdate).AddDays(1);
            }

            DateTime startOfWeek = givenDate.AddDays(-1 * Convert.ToInt32(givenDate.DayOfWeek)).AddDays(1);
            DateTime endOfWeek = startOfWeek.AddDays(5);

            TempData["sharedate"] = givenDate.Date.ToString("dd MMMM yyyy");
            var dx = givenDate.ToString("ddMMMyyyy");
            string xdx = dx;

            if (searchdate != null)
            {

                TempData["date"] = givenDate.ToString("ddd dd MMM, yyyy");
                var query = evct
                   .Where(ob => ob.Date.Date == givenDate.Date)
                   .Select(x => x.Date.Date.ToString())
                   .Distinct().AsQueryable();
                var xquery = query.FirstOrDefault();
                var eventinfo = await _context.Events.FirstOrDefaultAsync(x => x.Date.Date.ToString() == xquery);
                if (eventinfo != null)
                {
                    if (eventinfo.Note != null)
                    {
                        Desc = eventinfo.Note ?? "";
                    }
                }
                var sf = query.Contains("yyyy-MM-dd");
                Events = query;
                xdx = xdx + "single";

            }
            else
            {
                var query = evct
                  .Where(ob => startOfWeek <= ob.Date && ob.Date < endOfWeek)
                  .Select(x => x.Date.Date.ToString())
                  .Distinct().AsQueryable();
                var xquery = query.FirstOrDefault();
                var eventinfo = await _context.Events.FirstOrDefaultAsync(x => x.Date.Date.ToString() == xquery);
                if (eventinfo != null)
                {
                    if (eventinfo.Note != null)
                    {
                        Desc = eventinfo.Note ?? "";
                    }
                }
                Events = query;
            }
            var callbackUrl = Url.Page(
                         "/Dashboard/TM",
                         pageHandler: null,
                         values: new { area = "Participant", id = xdx },
                         protocol: Request.Scheme);

            string mi = $"{HtmlEncoder.Default.Encode(callbackUrl)}";
            TempData["link"] = mi;


            DateTime mondayOfLastWeek = givenDate.AddDays(-(int)givenDate.DayOfWeek - 6);
            DateTime mondayOfNextWeek = givenDate.AddDays(-(int)givenDate.DayOfWeek + 8);
            PreviousWeek = mondayOfLastWeek.Date.ToString("dd MMMM yyyy");
            NextWeek = mondayOfNextWeek.Date.ToString("dd MMMM yyyy");
            PreviousWeekTitle = "Previous " + mondayOfLastWeek.Date.ToString("dd MMMM") + " to " + mondayOfLastWeek.Date.AddDays(4).ToString("dd MMMM");
            NextWeekTitle = "Next " + mondayOfNextWeek.Date.ToString("dd MMMM") + " to " + mondayOfNextWeek.Date.AddDays(4).ToString("dd MMMM");
            Title = startOfWeek.ToString("dd") + " - " + endOfWeek.ToString("dd MMMM yyyy");
        }

    }
}
