
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
    public class NewsViewComponent : ViewComponent
    {
        private readonly UserManager<IdentityUser> _userManager;

        private readonly NIPSSDbContext _context;


        public NewsViewComponent(
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
           var News = _context.News.Include(x => x.Comments).OrderBy(x => x.Date).Take(3).ToList();

            ViewBag.News = News.ToList();
            return View();
        }
    }
}
