using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SEC44NIPSS.Data;
using SEC44NIPSS.Data.Model;

namespace SEC44NIPSS.Areas.Root.Pages.Main
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly SEC44NIPSS.Data.NIPSSDbContext _context;

        public IndexModel(SEC44NIPSS.Data.NIPSSDbContext context)
        {
            _context = context;
        }

        public Alumni Alumni { get; set; }
        public IList<Event> EventOnly { get; set; }
        public IList<ParlyReportCategory> ParlyReportCategories { get; set; }
        public IList<ParlyReportCategory> ParlyFolder { get; set; }
        public IQueryable<string> Events { get; set; }
        public string Desc { get; set; }
        public string PreviousWeek { get; set; }
        public string PreviousWeekTitle { get; set; }
        public string NextWeek { get; set; }
        public string NextWeekTitle { get; set; }
        public string Title { get; set; }

        public int TotalParticipant { get; set; }
        public int Male { get; set; }
        public int Female { get; set; }
        public int ProjectAssigned { get; set; }
        public int ProjectAssigned_Submitted { get; set; }
        public int LecturesDocuments { get; set; }
        public int RESOURCEMATERIALS { get; set; }
        public int GroupResearch { get; set; }
        public int INDIVIDUALESSAYS_Submitted { get; set; }
        public int INDIVIDUALESSAYS { get; set; }
        public int OTHERRESOURCEMATERIALS { get; set; }


         
        public async Task<IActionResult> OnGetAsync()
        {
          

            Alumni = await _context.Alumnis
                //.Include(x => x.StudyGroup)
                //.Include(x => x.Participants)

                //.ThenInclude(x => x.Profile)

                //.ThenInclude(x => x.User)


                //.Include(x => x.Executives)
                //.ThenInclude(x => x.Profile)

                //.Include(x => x.ManagingStaffs)
                //.ThenInclude(x => x.Profile)


                // .Include(x => x.DirectingStaffs)
                //.ThenInclude(x => x.Profile)
                //.Include(x => x.StudyGroup)
                //.ThenInclude(x=>x.SecParticipants)
                //.Include(x => x.SecProject)
                //.Include(x => x.SecPapers)
                //.ThenInclude(x => x.DocumentCategory)

                //.Include(x => x.CommitteeCategory)
                //.ThenInclude(x => x.Committees)

                .Where(m => m.Active == true).FirstOrDefaultAsync();

            if (Alumni == null)
            {
                return NotFound();
            }
            ParlyReportCategories = await _context.ParlyReportCategories.Include(x=>x.ParlyReportSubCategories).Include(x=>x.ParlyReportDocuments).Where(x => x.Show == true && x.FolderType == FolderType.Main).ToListAsync();
            ParlyFolder = await _context.ParlyReportCategories.Include(x => x.ParlyReportSubCategories).Include(x => x.ParlyReportDocuments).Where(x => x.Show == true && x.FolderType == FolderType.Parly).ToListAsync();
            TotalParticipant = await _context.Participants.Where(x => x.IsTrue == true).CountAsync();
            Male = await _context.Participants.Where(x => x.IsTrue == true && x.Profile.Gender.ToUpper() == "MALE").CountAsync();
            Female = await _context.Participants.Where(x => x.IsTrue == true && x.Profile.Gender.ToUpper() == "FEMALE").CountAsync();
            return Page();
        }

    }
}
