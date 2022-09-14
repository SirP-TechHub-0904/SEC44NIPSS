using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SEC44NIPSS.Data;
using SEC44NIPSS.Data.Model;

namespace SEC44NIPSS.Areas.ResolvedAdmin.Pages.ParticipantManager
{
    public class CreateModel : PageModel
    {
        private readonly SEC44NIPSS.Data.NIPSSDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public CreateModel(SEC44NIPSS.Data.NIPSSDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        [BindProperty]
        public long? AlumniId { get; set; }

        [BindProperty]
        public Profile Profile { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }


        }
        [BindProperty]
        public long ProfileId { get; set; }
        [BindProperty]
        public long StudyGroupId { get; set; }

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }


            ViewData["ProfileId"] = new SelectList(_context.Profiles.OrderBy(x=>x.FullName), "Id", "FullName");
            ViewData["StudyGroupId"] = new SelectList(_context.StudyGroups.Where(x => x.AlumniId == id), "Id", "Title");

            AlumniId = id;

            return Page();
        }

        [BindProperty]
        public SecParticipant SecParticipant { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {



            if (ProfileId > 0)
            {
                var profilx = await _context.Participants.FirstOrDefaultAsync(x => x.ProfileId == ProfileId);
                if (profilx != null)
                {
                    ViewData["ProfileId"] = new SelectList(_context.Profiles.OrderBy(x => x.FullName), "Id", "FullName");
                    ViewData["StudyGroupId"] = new SelectList(_context.StudyGroups.Where(x => x.AlumniId == AlumniId), "Id", "Title");

                    AlumniId = AlumniId;
                    return Page();
                }
            }

            var profilcheck = await _context.Profiles.Include(x => x.User).FirstOrDefaultAsync(x => x.User.Email == Input.Email);
            if (profilcheck != null)
            {

                ViewData["ProfileId"] = new SelectList(_context.Profiles.OrderBy(x => x.FullName), "Id", "FullName");
                ViewData["StudyGroupId"] = new SelectList(_context.StudyGroups.Where(x => x.AlumniId == AlumniId), "Id", "Title");

                AlumniId = AlumniId;
                return Page();
            }


            Profile.PXI = CreateRandomPassword();

            var user = new IdentityUser { UserName = Input.Email, Email = Input.Email };
            var result = await _userManager.CreateAsync(user, Profile.PXI);
            if (result.Succeeded)
            {

                Profile.DateRegistered = DateTime.UtcNow.AddHours(1);

                var dx = Profile.DateRegistered.ToString("ddMMMdddmmtt");
                Random num = new Random();

                // Create new string from the reordered char array
                string rand = new string(dx.ToCharArray().
                                OrderBy(s => (num.Next(2) % 2) == 0).ToArray());
                var chx = rand;

                Profile.ProfileHandler = chx;
                Profile.UserId = user.Id;
                _context.Profiles.Add(Profile);
                await _context.SaveChangesAsync();

                SecParticipant sec = new SecParticipant();
                sec.AlumniId = Profile.AlumniId;
                sec.ProfileId = Profile.Id;
                sec.StudyGroupId = StudyGroupId;
                _context.Participants.Add(sec);
                await _context.SaveChangesAsync();

            }
            return RedirectToPage("/ParticipantManager/Index", new { id = Profile.AlumniId });
        }
        private static string CreateRandomPassword(int length = 15)
        {
            // Create a string of characters, numbers, special characters that allowed in the password  
            string validChars = "ABCDEFGHJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789@#$";
            Random random = new Random();

            // Select one random character at a time from the string  
            // and create an array of chars  
            char[] chars = new char[length];
            for (int i = 0; i < length; i++)
            {
                chars[i] = validChars[random.Next(0, validChars.Length)];
            }
            return new string(chars);
        }

    }
}
