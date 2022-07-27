
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

namespace SEC44NIPSS.Pages.Shared.ViewComponents
{
    public class TimeTableViewComponent : ViewComponent
    {
        private readonly UserManager<IdentityUser> _userManager;

        private readonly NIPSSDbContext _context;


        public TimeTableViewComponent(
            UserManager<IdentityUser> userManager,
            NIPSSDbContext context
           
            /*IEmailSender emailSender*/)
        {
            _userManager = userManager;
            _context = context;
        }

        public string UserInfo{ get; set; }

        public async Task<IViewComponentResult> InvokeAsync(string date)
        {

            var datet = await _context.Events.Where(x => x.Date.Date.ToString() == date).ToListAsync();
            var datex = await _context.Events.FirstOrDefaultAsync(x => x.Date.Date.ToString() == date);
          ViewBag.datete = datex.EventDate;
            return View(datet);
        }
    }
}
//@model ICollection<Exwhyzee.AhiomaDashboard.EntityFramework.Tables.Category>

