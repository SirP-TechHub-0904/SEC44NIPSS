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

namespace SEC44NIPSS.Areas.Admin.Pages.ProfileAccount
{
    [Authorize(Roles = "mSuperAdmin,Admin")]

    public class IndexModel : PageModel
    {
        private readonly SEC44NIPSS.Data.NIPSSDbContext _context;

        public IndexModel(SEC44NIPSS.Data.NIPSSDbContext context)
        {
            _context = context;
        }

        public IList<Profile> Profile { get;set; }
        public int Participant { get;set; }
        public int Staff { get;set; }

        public async Task OnGetAsync()
        {

            IQueryable<Profile> profilex = from s in _context.Profiles
                                           .Include(x=>x.User)
                                           .Include(x=>x.StudyGroupMemeber)
                                           .ThenInclude(x=>x.StudyGroup)
                                           .Where(x => x.User.Email != "jinmcever@gmail.com").OrderByDescending(x => x.FullName)
                                     select s;
            Profile = await profilex.ToListAsync();
            //var xProfile = await profilex.ToListAsync();

            //int xd = 1;
            //foreach (var x in xProfile)
            //{
            //    xd++;
            //    x.DateRegistered = DateTime.UtcNow.AddHours(1).AddDays(-50);
            //    x.DateRegistered = x.DateRegistered.AddDays(xd);
            //   var dx = x.DateRegistered.ToString("ddMMMdddmmtt");
            //                    Random num = new Random();

            //    // Create new string from the reordered char array
            //    string rand = new string(dx.ToCharArray().
            //                    OrderBy(s => (num.Next(2) % 2) == 0).ToArray());

            //    var chx = rand;

            //    x.ProfileHandler = chx;
               
            //    _context.Attach(x).State = EntityState.Modified;

            //}
            //await _context.SaveChangesAsync();

            Participant = await profilex.Where(x => x.AccountRole == "Participant").CountAsync();
            Staff = await profilex.Where(x => x.AccountRole == "Staff").CountAsync();
        }
       


    }

}
