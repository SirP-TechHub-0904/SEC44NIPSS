using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SEC44NIPSS.Data.Model;

namespace SEC44NIPSS.Areas.Participant.Pages.Dashboard
{
    [Authorize(Roles = "Participant")]

    public partial class ProfileModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly Data.NIPSSDbContext _context;

        public ProfileModel(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager, Data.NIPSSDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }


        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public Profile Profile { get; set; }

        [BindProperty]
        public string GroupPosition { get; set; }

        [BindProperty]
        public long GroupId { get; set; }

        [BindProperty]
        public long GroupMemberId { get; set; }
        [BindProperty]
        public string GroupName { get; set; }
        public StudyGroupMemeber StudyGroup { get; set; }


        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
            Profile = await _context.Profiles.Include(x=>x.MyGallery).FirstOrDefaultAsync(x => x.UserId == user.Id);
            StudyGroup = await _context.StudyGroupMemebers.Include(x=>x.StudyGroup).FirstOrDefaultAsync(x => x.ProfileId == Profile.Id);
            if (StudyGroup != null)
            {
                GroupPosition = StudyGroup.Position;
                GroupName = StudyGroup.StudyGroup.Title;
                GroupMemberId = StudyGroup.Id;
            }
            ViewData["StudyGroupId"] = new SelectList(_context.StudyGroups, "Id", "Title");
            ViewData["StateId"] = new SelectList(_context.States, "StateName", "StateName");


            return Page();
        }
        public List<SelectListItem> LgaList { get; set; }
        
        public async Task<JsonResult> OnGetLGA(string id)
        {

            List<LocalGoverment> lga = new List<LocalGoverment>();

            var query = await _context.LocalGoverments.Include(x=>x.States).Where(x=>x.States.StateName == id).ToListAsync();


            LgaList = query.Select(a =>
                                new SelectListItem
                                {
                                    Value = a.LGAName,
                                    Text = a.LGAName
                                }).ToList();
            return new JsonResult(LgaList);
        }


        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            try
            {
                var pro = await _context.Profiles.FirstOrDefaultAsync(x => x.Id == Profile.Id);
                pro.LGA = Profile.LGA;
                pro.StateOfOrigin = Profile.StateOfOrigin;
                pro.ResidenceAddress = Profile.ResidenceAddress;
                pro.Title = Profile.Title;
                pro.FullName = Profile.FullName;
                pro.DOB = Profile.DOB;
                pro.PsNumber = Profile.PsNumber;
                pro.Sponsor = Profile.Sponsor;
                pro.Gender = Profile.Gender;
                pro.OfficeAddress = Profile.OfficeAddress;
                pro.ProfileUpdateLevel = ProfileUpdateLevel.ONE;
                pro.ProfileUpdateFirstTime = true;
                _context.Attach(pro).State = EntityState.Modified;

                try
                {
                    var group = await _context.StudyGroupMemebers.FirstOrDefaultAsync(x => x.Id == GroupMemberId);
                    group.Position = GroupPosition;
                    _context.Attach(group).State = EntityState.Modified;
                }catch(Exception f)
                {
                    TempData["alert"] = "Your not yet assigned to a Group.";
                }
                await _context.SaveChangesAsync();
                TempData["alert"] = "Profile Updated Successfull";
                return RedirectToPage("./Index");
            }
            catch(Exception c)
            {
                TempData["alert"] = "Unable to Updated Successfull";
            }
            return RedirectToPage("./Profile");
        }

    }

}
