﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SEC44NIPSS.Data;
using SEC44NIPSS.Data.Model;

namespace SEC44NIPSS.Areas.ResolvedAdmin.Pages.ParticipantDocumentCategoryPage
{
    public class DetailsModel : PageModel
    {
        private readonly SEC44NIPSS.Data.NIPSSDbContext _context;

        public DetailsModel(SEC44NIPSS.Data.NIPSSDbContext context)
        {
            _context = context;
        }

        public ParticipantDocumentCategory ParticipantDocumentCategory { get; set; }

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ParticipantDocumentCategory = await _context.ParticipantDocumentCategories.FirstOrDefaultAsync(m => m.Id == id);

            if (ParticipantDocumentCategory == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
