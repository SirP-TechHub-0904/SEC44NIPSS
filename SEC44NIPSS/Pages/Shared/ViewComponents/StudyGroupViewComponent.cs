
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization;
using SEC44NIPSS.Data.Model;
using SEC44NIPSS.Data;

namespace Pestcontrol.Pages.Shared.ViewComponents
{
    public class StudyGroupViewComponent : ViewComponent
    {
        private readonly UserManager<IdentityUser> _userManager;

        private readonly NIPSSDbContext _context;


        public StudyGroupViewComponent(
            UserManager<IdentityUser> userManager,
            NIPSSDbContext context
           
            /*IEmailSender emailSender*/)
        {
            _userManager = userManager;
            _context = context;
        }

        public string UserInfo{ get; set; }

        public async Task<IViewComponentResult> InvokeAsync(long? id)
        {
            if(id != null)
            {
                var item = await _context.TourSubCategories.Include(x => x.StudyGroup).FirstOrDefaultAsync(x => x.Id == id);
                if (item.StudyGroupId != null)
                {
                    TempData["data"] = item.StudyGroup.Title;
                    TempData["id"] = item.StudyGroupId;
                }
            }
          
            return View();
        }
    }
}
