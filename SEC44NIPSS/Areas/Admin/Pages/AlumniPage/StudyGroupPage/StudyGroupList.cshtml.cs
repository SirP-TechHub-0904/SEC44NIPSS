using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SEC44NIPSS.Data;
using SEC44NIPSS.Data.Model;

namespace SEC44NIPSS.Areas.Admin.Pages.AlumniPage.StudyGroupPage
{
    public class StudyGroupListModel : PageModel
    {
        private readonly SEC44NIPSS.Data.NIPSSDbContext _context;

        public StudyGroupListModel(SEC44NIPSS.Data.NIPSSDbContext context)
        {
            _context = context;
        }

        public IList<StudyGroup> StudyGroup { get;set; }

        public Alumni Alumni { get; set; }
        public async Task OnGetAsync(long id)
        {
            StudyGroup = await _context.StudyGroups
                .Include(e => e.Alumni)
                .Where(x => x.AlumniId == id)
                .ToListAsync();

            Alumni = await _context.Alumnis.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
