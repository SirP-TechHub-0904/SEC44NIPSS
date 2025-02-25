﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using SEC44NIPSS.Data;
using SEC44NIPSS.Data.Model;

namespace SEC44NIPSS.Areas.Admin.Pages.Rapid.Options
{
    public class CreateModel : PageModel
    {
        private readonly SEC44NIPSS.Data.NIPSSDbContext _context;

        public CreateModel(SEC44NIPSS.Data.NIPSSDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["RapidQuestionId"] = new SelectList(_context.RapidQuestions, "Id", "Id");
            return Page();
        }

        [BindProperty]
        public RapidOption RapidOption { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.RapidOption.Add(RapidOption);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
