using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SEC44NIPSS.Data;
using SEC44NIPSS.Data.Model;

namespace SEC44NIPSS.Areas.Root.Pages.LectureNote
{
    public class TimetableEventModel : PageModel
    {
        private readonly SEC44NIPSS.Data.NIPSSDbContext _context;

        public TimetableEventModel(SEC44NIPSS.Data.NIPSSDbContext context)
        {
            _context = context;
        }

        public IList<Event> Event { get;set; }

        public async Task OnGetAsync()
        {
            Event = await _context.Events.OrderBy(x=>x.Date).ToListAsync();
        }
    }
}
