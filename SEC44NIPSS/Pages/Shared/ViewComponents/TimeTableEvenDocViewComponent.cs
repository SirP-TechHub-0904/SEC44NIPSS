
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
    public class TimeTableEvenDocViewComponent : ViewComponent
    {
        private readonly UserManager<IdentityUser> _userManager;

        private readonly NIPSSDbContext _context;


        public TimeTableEvenDocViewComponent(
            UserManager<IdentityUser> userManager,
            NIPSSDbContext context
           
            /*IEmailSender emailSender*/)
        {
            _userManager = userManager;
            _context = context;
        }

        public string UserInfo{ get; set; }

        public async Task<IViewComponentResult> InvokeAsync(long id)
        {

            var datet = await _context.Documents.FirstOrDefaultAsync(x => x.EventId == id);
            if(datet != null)
            {
                if (String.IsNullOrEmpty(datet.FileName))
                {
                    TempData["empty"] = "Empty";
                }
                else
                {
                    TempData["found"] = datet.FileName;
                }
            }
            else
            {
                TempData["empty"] = "Empty";
            }

            return View(datet);
        }
    }
}
//@model ICollection<Exwhyzee.AhiomaDashboard.EntityFramework.Tables.Category>

