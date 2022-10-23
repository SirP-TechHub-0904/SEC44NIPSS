using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SEC44NIPSS.Data;
using SEC44NIPSS.Data.Model;

namespace SEC44NIPSS.Areas.Root.Pages.ParticipantDocumentPage
{
    public class UpdateModel : PageModel
    {
        private readonly SEC44NIPSS.Data.NIPSSDbContext _context;

        public UpdateModel(SEC44NIPSS.Data.NIPSSDbContext context)
        {
            _context = context;
        }

        public ParticipantDocument ParticipantDocument { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            //var xProfile = await _context.Participants.Where(x=>x.AlumniId ==null).ToListAsync();
            //var d = "";
            ////
            ////var xProfile = await _context.Profiles
            ////    .Include(p => p.User).Include(x=>x.StudyGroupMemeber).Where(x => x.OfficialRoleStatus== OfficialRoleStatus.Participant).ToListAsync();
            //foreach (var op in xProfile)
            //{
            //    op.AlumniId = 1;
            //    _context.Attach(op).State = EntityState.Modified;

            //    }
            //await _context.SaveChangesAsync();



            var profile = await _context.Participants.ToListAsync();
            foreach (var pp in profile)
            {
                var prod = await _context.Participants.FirstOrDefaultAsync(x => x.Id == pp.Id);
                var poff = await _context.ParticipantDocumentCategories.ToListAsync();
                foreach(var mp in poff)
                {
                    ParticipantDocument pdoc = new ParticipantDocument();
                    pdoc.ParticipantId = prod.Id;
                    pdoc.ParticipantDocumentCategoryId = mp.Id;
                    _context.ParticipantDocuments.Add(pdoc);

                }
                

            }
            await _context.SaveChangesAsync();

            //ParticipantDocument = await _context.ParticipantDocuments
            //    .Include(p => p.ParticipantDocumentCategory).FirstOrDefaultAsync(m => m.Id == id);

            //if (ParticipantDocument == null)
            //{
            //    return NotFound();
            //}
            return Page();
        }
    }
}
