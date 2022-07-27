using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SEC44NIPSS.Data;
using SEC44NIPSS.Data.Model;

namespace SEC44NIPSS.Areas.NIPSS.Pages.MaintainancePage
{
    public class CloseTicketModel : PageModel
    {
        private readonly SEC44NIPSS.Data.NIPSSDbContext _context;

        public CloseTicketModel(SEC44NIPSS.Data.NIPSSDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Ticket Ticket { get; set; }

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Ticket = await _context.Tickets
                .Include(t => t.ApprovedBy)
                .Include(t => t.ForwardedTo)
                .Include(t => t.JobCompletionCertifiedBy)
                .Include(t => t.ReceivedAndPassTo)
                .Include(t => t.User).FirstOrDefaultAsync(m => m.Id == id);

            if (Ticket == null)
            {
                return NotFound();
            }
          
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var tick = await _context.Tickets.FindAsync(Ticket.Id);
            tick.Closed = true;
            tick.ClosedTime = DateTime.UtcNow.AddHours(1);

            _context.Attach(tick).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TicketExists(Ticket.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool TicketExists(long id)
        {
            return _context.Tickets.Any(e => e.Id == id);
        }
    }
}
