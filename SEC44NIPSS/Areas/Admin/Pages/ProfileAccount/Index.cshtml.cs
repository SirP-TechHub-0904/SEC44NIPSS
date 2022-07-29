using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SEC44NIPSS.Data;
using SEC44NIPSS.Data.Model;

namespace SEC44NIPSS.Areas.Admin.Pages.ProfileAccount
{
    [Authorize(Roles = "mSuperAdmin,Admin")]

    public class IndexModel : PageModel
    {
        private readonly SEC44NIPSS.Data.NIPSSDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public IndexModel(SEC44NIPSS.Data.NIPSSDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IList<Profile> Profile { get; set; }
        public int Participant { get; set; }
        public int Staff { get; set; }
        public int DS { get; set; }
        public int MS { get; set; }

        public async Task OnGetAsync()
        {

            IQueryable<Profile> profilex = from s in _context.Profiles
                                           .Include(x => x.User)
                                           .Include(x => x.StudyGroupMemeber)
                                           .ThenInclude(x => x.StudyGroup)
                                           .Where(x => x.User.Email != "jinmcever@gmail.com").OrderByDescending(x => x.FullName)
                                           select s;
            Profile = await profilex.ToListAsync();
            //var Executive = await _context.Executive.Include(x => x.Profile).ThenInclude(x => x.StudyGroupMemeber).ThenInclude(x => x.StudyGroup).Include(x => x.Alumni).OrderBy(x => x.SortOrder).Where(x => x.Alumni.Active == true).ToListAsync();

            // foreach (var x in Executive)
            // {
            //     var pro = await _context.Profiles.FindAsync(x.ProfileId);
            //     pro.IsExecutive = true;
            //     pro.Position = x.Position;
            //     pro.SortOrder = Convert.ToInt32(x.SortOrder);
            //     _context.Attach(pro).State = EntityState.Modified;

            // }
            // await _context.SaveChangesAsync();

            //var xProfile = await profilex.Include(x => x.User).Where(x => x.OfficialRoleStatus == OfficialRoleStatus.Participant).ToListAsync();

            //foreach (var x in xProfile)
            //{

            //    Document d = new Document();
            //    d.DocumentCategoryId = 3;
            //    d.ProfileId = x.Id;
            //    d.Description = "INDIVIDUAL RESEARCH PROJECT";
            //    d.Title = "PROJECT";

            //    _context.Documents.Add(d);

            //}
            //await _context.SaveChangesAsync();




            Participant = await profilex.Where(x => x.OfficialRoleStatus == OfficialRoleStatus.Participant).CountAsync();
            Staff = await profilex.Where(x => x.OfficialRoleStatus == OfficialRoleStatus.Staff).CountAsync();
            DS = await profilex.Where(x => x.OfficialRoleStatus == OfficialRoleStatus.DirectingStaff).CountAsync();
            MS = await profilex.Where(x => x.OfficialRoleStatus == OfficialRoleStatus.ManagingStaff).CountAsync();
        }



    }

}
