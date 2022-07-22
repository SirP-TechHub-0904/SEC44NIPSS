using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SEC44NIPSS.Data.Dtos;
using SEC44NIPSS.Data.Model;
using SEC44NIPSS.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SEC44NIPSS.Pages
{
    public class IndexModel : PageModel 
    {
        private readonly SEC44NIPSS.Data.NIPSSDbContext _context;
        private readonly INotificationService _notificationService;
        private readonly UserManager<IdentityUser> _userManager;


        public IndexModel(Data.NIPSSDbContext context, INotificationService notificationService, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _notificationService = notificationService;
            _userManager = userManager;
        }
        public IList<Executive> Executive { get; set; }
        public IList<NipssStaffListDto> NipssStaffList { get; set; }
        public IList<News> News { get; set; }
        public IList<CurrentAffair> CurrentAffair { get; set; }

        public IActionResult OnGetAsync()
        {
            Executive =  _context.Executive.Include(x=>x.Profile).Include(x => x.Alumni).OrderBy(x => x.SortOrder).Where(x=>x.Alumni.Active == true).Take(3).ToList();
             var nipssstaff = _context.Profiles.Include(x => x.User).Where(x => x.AccountRole == "ManagingStaff").OrderBy(x => x.SortOrder).Take(3).ToList();
            var output = nipssstaff.Select(x => new NipssStaffListDto
            {
                Id = x.Id,
               Position = x.Position,
               Fullname = x.Title + " "+ x.FullName,
               Photo =x.AboutProfile,
            });

            NipssStaffList = output.ToList();
            News = _context.News.Include(x=>x.Comments).OrderBy(x => x.Date).Take(3).ToList();

            return Page();
            
        }

        [BindProperty]
        public Contact Contact { get; set; }


        public int P1 { get; set; }
        public int P2 { get; set; }
        public int P3 { get; set; }
        public int P4 { get; set; }
        public int P5 { get; set; }
        public int P6 { get; set; }
        public int P7 { get; set; }
        public int P8 { get; set; }
        public int P9 { get; set; }
        public int P10 { get; set; }
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.


        public async Task<JsonResult> OnGetRunAccount(string devicex, string tokenid)
        {
            try
            {
                long id = 0;
                var user = await _userManager.GetUserAsync(User);
                if(user != null)
                {
                    var profile = await _context.Profiles.FirstOrDefaultAsync(x => x.UserId == user.Id);
                    id = profile.Id;
                }
                var getnotifystatus = await _context.UserToNotifys.Include(x=>x.Profile).FirstOrDefaultAsync(x => x.TokenId == tokenid);
                if (getnotifystatus != null)
                {
                    if (getnotifystatus.ProfileId == null)
                    {
                        getnotifystatus.ProfileId = id;
                        _context.Attach(getnotifystatus).State = EntityState.Modified;

                        await _context.SaveChangesAsync();


                        Notification nf = new Notification();
                        nf.DatetTime = DateTime.UtcNow.AddHours(1).AddMinutes(4);
                        nf.Message = "WELCOME "+getnotifystatus.Profile.FullName+". KINDLY ENABLE ALL YOUR NOTIFICATIONS TO ENABLE US NOTIFY YOU ON IMPORTANT MATTERS.";
                        nf.Title = "WELCOME TO SEC 44, 2022";
                        nf.UserToNotifyId = getnotifystatus.Id;
                        _context.Notifications.Add(nf);
                        await _context.SaveChangesAsync();
                    }
                }
                else
                {
                    UserToNotify n = new UserToNotify();
                    n.IsAndriod = true;
                    n.TokenId = tokenid;
                    if (id == 0)
                    {
                        n.ProfileId = null;
                    }
                    else
                    {
                        n.ProfileId = id;
                    }
                    _context.UserToNotifys.Add(n);
                    await _context.SaveChangesAsync();

                    Notification nf = new Notification();
                    nf.DatetTime = DateTime.UtcNow.AddHours(1).AddMinutes(4);
                    nf.Message = "WELCOME TO SEC 44. KINDLY ENABLE ALL YOUR NOTIFICATIONS TO ENABLE US NOTIFY YOU ON IMPORTANT MATTERS.";
                    nf.Title = "WELCOME TO SEC 44, 2022";
                    nf.UserToNotifyId = n.Id;
                    _context.Notifications.Add(nf);
                    await _context.SaveChangesAsync();

                }

                return null;
            }
            catch (Exception k)
            {
                return new JsonResult("not found");
            }
        }

    }
}
