using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SEC44NIPSS.Data;
using SEC44NIPSS.Data.Model;

namespace SEC44NIPSS.Areas.NIPSS.Pages.Admin
{
    [Authorize(Roles = "Admin")]

    public class IndexModel : PageModel
    {
        private readonly SEC44NIPSS.Data.NIPSSDbContext _context;

        public IndexModel(SEC44NIPSS.Data.NIPSSDbContext context)
        {
            _context = context;
        }

        public IList<TourCategory> TourCategories { get;set; }
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


        public int TotalStaff { get; set; }
        public int StaffMale { get; set; }
        public int StaffFemale { get; set; }

        public int TotalDStaff { get; set; }
        public int DStaffMale { get; set; }
        public int DStaffFemale { get; set; }

        public int TotalMStaff { get; set; }
        public int MStaffMale { get; set; }
        public int MStaffFemale { get; set; }

        public IList<DocumentCategory> DocumentCategory { get; set; }
        public IList<Alumni> Alumni { get; set; }
        public IList<StudyGroup> StudyGroup { get; set; }

        public async Task OnGetAsync()
        {
            StudyGroup = await _context.StudyGroups.Include(x=>x.StudyGroupMemebers).ThenInclude(x => x.Profile).Include(x => x.Alumni).Where(x=>x.Alumni.Active == true).ToListAsync();
            TourCategories = await _context.TourCategories.Include(x=>x.TourSubCategories).Include(x => x.Alumni).Where(x=>x.Alumni.Active == true).ToListAsync();
            Alumni = await _context.Alumnis.Include(x=>x.SecProject)
                .Include(x => x.TourCategories)
                .Include(x => x.Executives)
                .Include(x => x.StudyGroup)
                .Include(x => x.Profiles).Where(x=>x.Active==false).OrderBy(x=>x.SortOrder)
                .ToListAsync();
            var part = await _context.Profiles.Include(x => x.Alumni).Where(x => x.Alumni.Active == true).ToListAsync();
            var acc = await _context.Profiles.ToListAsync();
            TotalParticipant = part.Where(x => x.AccountRole == "Participant").Count();
            Male = part.Where(x => x.AccountRole == "Participant").Where(x => x.Gender != null && x.Gender.ToLower() == "male").Count();
            Female = part.Where(x => x.AccountRole == "Participant").Where(x => x.Gender != null && x.Gender.ToLower() == "female").Count();

            TotalStaff = acc.Where(x => x.AccountRole == "Staff").Count();
            StaffMale = acc.Where(x => x.AccountRole == "Staff").Where(x => x.Gender != null && x.Gender.ToLower() == "male").Count();
            StaffFemale = acc.Where(x => x.AccountRole == "Staff").Where(x => x.Gender != null && x.Gender.ToLower() == "female").Count();
            
            
            TotalDStaff = acc.Where(x => x.AccountRole == "DirectingStaff").Count();
            DStaffMale = acc.Where(x => x.AccountRole == "DirectingStaff").Where(x => x.Gender != null && x.Gender.ToLower() == "male").Count();
            DStaffFemale = acc.Where(x => x.AccountRole == "DirectingStaff").Where(x => x.Gender != null && x.Gender.ToLower() == "female").Count();

            TotalMStaff = acc.Where(x => x.AccountRole == "ManagingStaff").Count();
            MStaffMale = acc.Where(x => x.AccountRole == "ManagingStaff").Where(x => x.Gender != null && x.Gender.ToLower() == "male").Count();
            MStaffFemale = acc.Where(x => x.AccountRole == "ManagingStaff").Where(x => x.Gender != null && x.Gender.ToLower() == "female").Count();


            var doc = await _context.Documents.Include(x => x.DocumentCategory).ToListAsync();
            ProjectAssigned = doc.Where(x => x.DocumentCategory.Title.Contains("INDIVIDUAL RESEARCH PROJECTS")).Count();
            ProjectAssigned_Submitted = doc.Where(x => x.DocumentCategory.Title.Contains("INDIVIDUAL RESEARCH PROJECTS")).Where(x => x.FileName != null).Count();
            LecturesDocuments = doc.Where(x => x.DocumentCategory.Title.Contains("LECTURE NOTES")).Count();
            RESOURCEMATERIALS = doc.Where(x => x.DocumentCategory.Title.Contains("RESOURCE MATERIALS ON LOCAL GOVERNMENT")).Count();
            GroupResearch = doc.Where(x => x.DocumentCategory.Title.Contains("GROUP RESEARCH POLICY PAPERS")).Count();
            INDIVIDUALESSAYS = doc.Where(x => x.DocumentCategory.Title.Contains("INDIVIDUAL ESSAYS")).Count();
            INDIVIDUALESSAYS_Submitted = doc.Where(x => x.DocumentCategory.Title.Contains("INDIVIDUAL ESSAYS")).Where(x => x.FileName != null).Count();
            OTHERRESOURCEMATERIALS = doc.Where(x => x.DocumentCategory.Title.Contains("OTHER RESOURCE MATERIALS")).Count();


            DocumentCategory = await _context.DocumentCategories.Include(x => x.Documents).Include(x => x.Alumni).Where(x => x.Alumni.Active == true).ToListAsync();

        }
    }
}
