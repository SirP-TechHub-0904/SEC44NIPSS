using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SEC44NIPSS.Data.Model;

namespace SEC44NIPSS.Areas.Participant.Pages.TourPage
{
    public class InstitutionModel : PageModel
    {
        private readonly SEC44NIPSS.Data.NIPSSDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IHostingEnvironment _hostingEnv;

        public InstitutionModel(SEC44NIPSS.Data.NIPSSDbContext context, UserManager<IdentityUser> userManager, IHostingEnvironment hostingEnv)
        {
            _context = context;
            _userManager = userManager;
            _hostingEnv = hostingEnv;
        }

        public IList<TourPost> TourPostList { get; set; }
        public TourPost TourPost { get; set; }
        public IList<TourSubCategory> TourSubCategories { get; set; }
        public string Title { get; set; }
        [BindProperty]
        public long TourFileId { get; set; }

        [BindProperty]
        public string UserId { get; set; }
        public TourSubCategory SubCat { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            TourPostList = await _context.TourPosts
                .Include(t => t.TourPostType)
                .Include(t => t.TourSubCategory).ToListAsync();
            UserId = user.Id;
            ViewData["TourPostTypeId"] = new SelectList(_context.TourPostTypes, "Id", "PostType");

            var profile = await _context.Profiles.FirstOrDefaultAsync(x => x.UserId == UserId);
            var studygroup = await _context.StudyGroupMemebers.FirstOrDefaultAsync(x => x.ProfileId == profile.Id);
            var subcat = await _context.TourSubCategories.FirstOrDefaultAsync(x => x.StudyGroupId == studygroup.StudyGroupId && x.Id == 1);


            SubCat = await _context.TourSubCategories.Include(x => x.TourCategory).FirstOrDefaultAsync(x => x.Id == subcat.Id);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            int imgCount = 0;
            TourPost TourPost = new TourPost();


            if (HttpContext.Request.Form.Files != null && HttpContext.Request.Form.Files.Count > 0)
            {
                var newFileName = string.Empty;
                var newFileNameThumbnail = string.Empty;
                var filePath = string.Empty;
                var filePathThumbnail = string.Empty;
                string pathdb = string.Empty;
                var files = HttpContext.Request.Form.Files;
                foreach (var file in files)
                {

                    if (file.Length > 0)
                    {
                        filePath = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                        filePathThumbnail = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                        imgCount++;
                        var now = DateTime.Now;
                        string nameproduct = "Post";
                        var uniqueFileName = $"{now.Millisecond}{now.Minute}{now.Second}{now.Day}-".Trim() + nameproduct;

                        var fileExtension = Path.GetExtension(filePath);

                        newFileName = uniqueFileName + fileExtension;

                        // if you wish to save file path to db use this filepath variable + newFileName
                        var fileDbPathName = $"/Post/".Trim();

                        filePath = $"{_hostingEnv.WebRootPath}{fileDbPathName}".Trim();

                        if (!(Directory.Exists(filePath)))
                            Directory.CreateDirectory(filePath);

                        var fileName = "";
                        fileName = filePath + $"{newFileName}".Trim();

                        using (FileStream fsa = System.IO.File.Create(fileName))
                        {
                            file.CopyTo(fsa);
                            fsa.Flush();
                        }



                        TourPost.Photo = $"{fileDbPathName}{newFileName}";

                        #region Save Image Propertie to Db

                        #endregion

                        if (imgCount >= 5)
                            break;
                    }
                }
            }
            var profile = await _context.Profiles.FirstOrDefaultAsync(x => x.UserId == UserId);
            var studygroup = await _context.StudyGroupMemebers.FirstOrDefaultAsync(x => x.ProfileId == profile.Id);
            var subcat = await _context.TourSubCategories.FirstOrDefaultAsync(x => x.StudyGroupId == studygroup.StudyGroupId && x.Id == 1);
            TourPost.TourSubCategoryId = subcat.Id;
            if (TourPost.Photo.Contains("mp4"))
            {
                TourPost.PostFileType = PostFileType.VIDEO;
            }else if (TourPost.Photo.Contains("jpeg") || TourPost.Photo.Contains("jpg") || TourPost.Photo.Contains("png"))
            {
                TourPost.PostFileType = PostFileType.IMG;

            }
            TourPost.Date = DateTime.UtcNow;
            TourPost.UserId = profile.UserId;
            TourPost.TourPostTypeId = TourFileId;
            _context.TourPosts.Add(TourPost);
            await _context.SaveChangesAsync();
            TempData["alert"] = "Upload Successfull";

            return RedirectToPage("./Institution");
        }
    }
}
