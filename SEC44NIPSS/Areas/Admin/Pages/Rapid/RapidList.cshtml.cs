using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SEC44NIPSS.Data;
using SEC44NIPSS.Data.Model;

namespace SEC44NIPSS.Areas.Admin.Pages.Rapid
{
    public class RapidListModel : PageModel
    {
        private readonly SEC44NIPSS.Data.NIPSSDbContext _context;

        public RapidListModel(SEC44NIPSS.Data.NIPSSDbContext context)
        {
            _context = context;
        }

        public IList<UserAnswer> UserAnswer { get;set; }

        public async Task OnGetAsync(string id)
        {
            if (String.IsNullOrEmpty(id))
            {
                UserAnswer = await _context.UserAnswers.Include(x => x.Profile).ToListAsync();

            }
            else
            {
                UserAnswer = await _context.UserAnswers.Include(x => x.Profile).Where(x => x.Title == id).OrderByDescending(x=>x.Date).ToListAsync();

            }
        }
    }
}
