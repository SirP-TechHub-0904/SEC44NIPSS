﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SEC44NIPSS.Data;
using SEC44NIPSS.Data.Model;

namespace SEC44NIPSS.Areas.ResolvedAdmin.Pages.ParlyCategory.SubCategory
{
    public class FoldersModel : PageModel
    {
        private readonly SEC44NIPSS.Data.NIPSSDbContext _context;

        public FoldersModel(SEC44NIPSS.Data.NIPSSDbContext context)
        {
            _context = context;
        }

        public IList<ParlyReportSubCategory> ParlyReportSubCategory { get;set; }
        public ParlyReportCategory ParlyReportCategory { get; set; }


        public async Task OnGetAsync(long id)
        {
            ParlyReportSubCategory = await _context.ParlyReportSubCategories
                .Include(p => p.ParlyReportCategory).Include(p => p.ParlySubTwoCategories).Where(x=>x.ParlyReportCategoryId == id).OrderBy(x => x.SortOrder).ToListAsync();

            ParlyReportCategory = await _context.ParlyReportCategories.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
