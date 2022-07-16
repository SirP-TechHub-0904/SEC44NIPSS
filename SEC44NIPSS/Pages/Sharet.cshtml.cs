using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SEC44NIPSS.Data;
using SEC44NIPSS.Data.Model;

namespace SEC44NIPSS.Pages
{
    public class SharetModel : PageModel
    {
        private readonly SEC44NIPSS.Data.NIPSSDbContext _context;

        public SharetModel(SEC44NIPSS.Data.NIPSSDbContext context)
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

        public async Task OnGetAsync(string id = null, string k = null)
        {
            //name="searchdatecancel" value="cancel"
            IQueryable<Event> evct = from s in _context.Events.OrderByDescending(x => x.Date)
                                         //.Where(x=>x.Date.DayOfWeek == DateTime.UtcNow.DayOfWeek)
                                     select s;
            string searchdate = "";
            if (k == "single")
            {
                searchdate = id;
            }
            DateTime givenDate = DateTime.Today;
            if (id != null)
            {

                givenDate = DateTime.Parse(id).AddDays(1);
            }


            DateTime startOfWeek = givenDate.AddDays(-1 * Convert.ToInt32(givenDate.DayOfWeek)).AddDays(1);
            DateTime endOfWeek = startOfWeek.AddDays(5);

            var dx = givenDate.ToString("ddMMyyyy");
            string xdx = "";
            if (k != null)
            {
                xdx = dx + "single";
            }

            var callbackUrl = Url.Page(
                            "/Dashboard/Timetable",
                            pageHandler: null,
                            values: new { area = "Participant", id= xdx },
                            protocol: Request.Scheme);

            string mi = $"'{HtmlEncoder.Default.Encode(callbackUrl)}'";
            TempData["link"] = mi;

        }
    }
}
