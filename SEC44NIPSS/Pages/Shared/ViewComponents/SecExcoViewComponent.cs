
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
using SEC44NIPSS.Data.Dtos;

namespace SEC44NIPSS.Pages.Shared.ViewComponents
{
    public class SecExcoViewComponent : ViewComponent
    {
        private readonly UserManager<IdentityUser> _userManager;

        private readonly NIPSSDbContext _context;


        public SecExcoViewComponent(
            UserManager<IdentityUser> userManager,
            NIPSSDbContext context
           
            /*IEmailSender emailSender*/)
        {
            _userManager = userManager;
            _context = context;
        }

        public string UserInfo{ get; set; }

        public async Task<IViewComponentResult> InvokeAsync()
        {
           var Executive = _context.Executive.Include(x => x.Profile).Include(x => x.Alumni).OrderBy(x => x.SortOrder).Where(x => x.Alumni.Active == true).Take(3).ToList();


            ViewBag.Executive = Executive.ToList();
            return View();
        }
    }
}
