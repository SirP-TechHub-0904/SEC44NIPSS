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
    public class SEC44ExcoModel : PageModel
    {
        private readonly SEC44NIPSS.Data.NIPSSDbContext _context;

        public SEC44ExcoModel(SEC44NIPSS.Data.NIPSSDbContext context)
        {
            _context = context;
        }

        public IList<Profile> Profile { get;set; }

        public async Task OnGetAsync()
        {
            Profile = await _context.Profiles
                                        .Include(p => p.Alumni)
                                        .Include(p => p.StudyGroupMemeber)
                                        .ThenInclude(x => x.StudyGroup)
                                        .Include(p => p.User).Where(x => x.Alumni.Active == true).Where(x => x.OfficialRoleStatus == OfficialRoleStatus.Participant && x.IsExecutive == true)
                                        .OrderBy(x=>x.SortOrder).ToListAsync();
        }
    }
}
