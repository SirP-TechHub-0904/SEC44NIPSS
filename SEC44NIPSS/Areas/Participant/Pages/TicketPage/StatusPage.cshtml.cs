using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SEC44NIPSS.Data;
using SEC44NIPSS.Data.Model;

namespace SEC44NIPSS.Areas.Participant.Pages.TicketPage
{
    public class StatusPageModel : PageModel
    {
        private readonly SEC44NIPSS.Data.NIPSSDbContext _context;

        public StatusPageModel(SEC44NIPSS.Data.NIPSSDbContext context)
        {
            _context = context;
        }

        public Ticket Ticket { get; set; }

        public async Task<IActionResult> OnGetAsync(string ticketnumber)
        {
           

            Ticket = await _context.Tickets
                .Include(t => t.ApprovedBy)
                .Include(t => t.ForwardedTo)
                .Include(t => t.JobCompletionCertifiedBy)
                .Include(t => t.ReceivedAndPassTo).FirstOrDefaultAsync(m => m.TicketNumber == ticketnumber);

            if (Ticket == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
