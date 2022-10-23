using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SEC44NIPSS.Data;
using SEC44NIPSS.Data.Model;

namespace SEC44NIPSS.Areas.Participant.Pages.Dashboard
{
    [Authorize]
    public class ParticipantListModel : PageModel
    {
        private readonly SEC44NIPSS.Data.NIPSSDbContext _context;

        public ParticipantListModel(SEC44NIPSS.Data.NIPSSDbContext context)
        {
            _context = context;
        }
        public string Gender { get; set; }
        public IList<Profile> Profile { get;set; }
        public IList<SecParticipant> Participant { get; set; }

        public async Task OnGetAsync(string gender = null)
        {
            //Profile = await _context.Profiles
            //    .Include(p => p.Alumni)
            //    .Include(p => p.User).Where(x => x.IsParticipant == true).ToListAsync();
            //if(gender != null)
            //{
            //    Gender = gender;
            //    Profile = Profile.Where(x => x.Gender != null && x.Gender.ToLower() == gender).ToList();
            //}

            Participant = await _context.Participants
               .Include(p => p.Alumni)
               .Include(p => p.Profile).Where(x=>x.Alumni.Active == true && x.IsTrue == true).ToListAsync();
        }
    }
}
