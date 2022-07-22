﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SEC44NIPSS.Data;
using SEC44NIPSS.Data.Model;

namespace SEC44NIPSS.Areas.Admin.Pages.Maintainance.Supervisors
{
    public class DeleteModel : PageModel
    {
        private readonly SEC44NIPSS.Data.NIPSSDbContext _context;

        public DeleteModel(SEC44NIPSS.Data.NIPSSDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public TicketSupervisor TicketSupervisor { get; set; }

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            TicketSupervisor = await _context.TicketSupervisor.FirstOrDefaultAsync(m => m.Id == id);

            if (TicketSupervisor == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            TicketSupervisor = await _context.TicketSupervisor.FindAsync(id);

            if (TicketSupervisor != null)
            {
                _context.TicketSupervisor.Remove(TicketSupervisor);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
