using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SEC44NIPSS.Data;
using SEC44NIPSS.Data.Model;

namespace SEC44NIPSS.Areas.Participant.Pages.Dashboard
{
    [Authorize]
    public class MyGalleryModel : PageModel
    {
        private readonly SEC44NIPSS.Data.NIPSSDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IHostingEnvironment _hostingEnv;


        public MyGalleryModel(SEC44NIPSS.Data.NIPSSDbContext context, UserManager<IdentityUser> userManager, IHostingEnvironment hostingEnv)
        {
            _context = context;
            _userManager = userManager;
            _hostingEnv = hostingEnv;
        }

        public IList<Gallery> Gallery { get;set; }

        public async Task OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            var profile = await _context.Profiles.FirstOrDefaultAsync(x => x.UserId == user.Id);
            Gallery = await _context.Galleries
                .Include(g => g.Profile).Where(x=>x.ProfileId == profile.Id).OrderByDescending(x=>x.Date).ToListAsync();
        }

        public async Task<IActionResult> OnPostAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

           var gallery = await _context.Galleries.FindAsync(id);

            if (gallery != null)
            {
               var filePath = $"{_hostingEnv.WebRootPath}".Trim();
                string fullPath = filePath + gallery.FilePath;
                if (System.IO.File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                }

                _context.Galleries.Remove(gallery);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./MyGallery");
        }


        public async Task<IActionResult> OnPostMake(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gallery = await _context.Galleries.FindAsync(id);

            if (gallery != null)
            {
                var profile = await _context.Profiles.FirstOrDefaultAsync(x => x.Id == gallery.ProfileId);
                profile.AboutProfile = gallery.FilePath;
                _context.Attach(profile).State = EntityState.Modified;

                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./MyGallery");
        }


        public async Task<IActionResult> OnPostActivity(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gallery = await _context.Galleries.FindAsync(id);

            if (gallery != null)
            {
                gallery.UseAsActivity = true;
                _context.Attach(gallery).State = EntityState.Modified;

                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./MyGallery");
        }


        public async Task<IActionResult> OnPostUnUse(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gallery = await _context.Galleries.FindAsync(id);

            if (gallery != null)
            {
                gallery.UseAsActivity = false;
                _context.Attach(gallery).State = EntityState.Modified;

                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./MyGallery");
        }

    }
}
