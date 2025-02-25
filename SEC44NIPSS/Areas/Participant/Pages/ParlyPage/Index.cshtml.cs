﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SEC44NIPSS.Data;
using SEC44NIPSS.Data.Model;

namespace SEC44NIPSS.Areas.Participant.Pages.ParlyPage
{
    public class IndexModel : PageModel
    {
        private readonly SEC44NIPSS.Data.NIPSSDbContext _context;

        public IndexModel(SEC44NIPSS.Data.NIPSSDbContext context)
        {
            _context = context;
        }

        public IList<ParlyReportCategory> ParlyReportCategory { get;set; }

        public async Task OnGetAsync()
        {
            ParlyReportCategory = await _context.ParlyReportCategories.Include(x => x.ParlyReportSubCategories).Include(x => x.ParlyReportDocuments).ToListAsync();
        }
    }
}
