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

namespace SEC44NIPSS.Areas.Root.Pages.SecProjectPage
{
    public class UpdateProjectModel : PageModel
    {
        private readonly SEC44NIPSS.Data.NIPSSDbContext _context;

        public UpdateProjectModel(SEC44NIPSS.Data.NIPSSDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public SecProject SecProject { get; set; }

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            SecProject = await _context.secProjects
                .Include(s => s.Alumni).FirstOrDefaultAsync(m => m.AlumniId == id);

            if (SecProject == null)
            {
                SecProject s = new SecProject();
                s.Title = "Name of Project";
                s.AlumniId = id;
                _context.secProjects.Add(s);
                await _context.SaveChangesAsync();

                SecProject = await _context.secProjects
                .Include(s => s.Alumni).FirstOrDefaultAsync(m => m.AlumniId == id);
            }

           //ViewData["AlumniId"] = new SelectList(_context.Alumnis, "Id", "Id");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(SecProject).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SecProjectExists(SecProject.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("/Main/Details", new { id = SecProject.AlumniId });
        }

        private bool SecProjectExists(long id)
        {
            return _context.secProjects.Any(e => e.Id == id);
        }
    }
}
