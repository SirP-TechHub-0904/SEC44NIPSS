using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SEC44NIPSS.Data;
using SEC44NIPSS.Data.Model;

namespace SEC44NIPSS.Areas.NIPSS.Pages.Admin
{
    public class AllUsersModel : PageModel
    {
        private readonly SEC44NIPSS.Data.NIPSSDbContext _context;

        public AllUsersModel(SEC44NIPSS.Data.NIPSSDbContext context)
        {
            _context = context;
        }

        public IList<Profile> Profile { get;set; }
        public int Participant { get; set; }
        public int Staff { get; set; }
        public int DS { get; set; }
        public int MS { get; set; }
        public async Task OnGetAsync()
        {
          

            Profile = await _context.Profiles
                                       .Include(p => p.Alumni)
                                       .Include(p => p.StudyGroupMemeber)
                                       .ThenInclude(x => x.StudyGroup)
                                       .Include(p => p.User).Where(x => x.User.Email != "jinmcever@gmail.com")
                                       .OrderBy(x => x.FullName).ToListAsync();

            Participant =  Profile.Where(x => x.OfficialRoleStatus == OfficialRoleStatus.Participant).Count();
            Staff =  Profile.Where(x => x.OfficialRoleStatus == OfficialRoleStatus.Staff).Count();
            DS =  Profile.Where(x => x.OfficialRoleStatus == OfficialRoleStatus.DirectingStaff).Count();
            MS =  Profile.Where(x => x.OfficialRoleStatus == OfficialRoleStatus.ManagingStaff).Count();
        }
    }
}
