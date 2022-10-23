using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SEC44NIPSS.Data;
using SEC44NIPSS.Data.Model;

namespace SEC44NIPSS.Areas.Root.Pages.Main
{
    public class AlumniModel : PageModel
    {
        private readonly SEC44NIPSS.Data.NIPSSDbContext _context;

        public AlumniModel(SEC44NIPSS.Data.NIPSSDbContext context)
        {
            _context = context;
        }

        public IList<Alumni> Alumni { get;set; }

        public async Task OnGetAsync()
        {
            Alumni = await _context.Alumnis
                .Include(x => x.Participants)
                .ThenInclude(x => x.Profile)
                .ThenInclude(x => x.User)

                .Include(x => x.Executives)

                .Include(x => x.ManagingStaffs)

                 .Include(x => x.SecProject)
                 .Include(x => x.StudyGroup)
                 //.Include(x => x.CommitteeCategory)


                 .Include(x => x.SecPapers)
                 .Include(x => x.Tours)

                 .OrderByDescending(x=>x.SortOrder)
                .ToListAsync();
        }
    }
}
