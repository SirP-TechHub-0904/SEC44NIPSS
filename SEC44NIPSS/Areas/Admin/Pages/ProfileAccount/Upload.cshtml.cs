using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SEC44NIPSS.Data.Model;


namespace SEC44NIPSS.Areas.Admin.Pages.ProfileAccount
{
        public partial class UploadModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly Data.NIPSSDbContext _context;
        private readonly IHostingEnvironment _hostingEnv;


        public UploadModel(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager, Data.NIPSSDbContext context, IHostingEnvironment hostingEnv)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
            _hostingEnv = hostingEnv;
        }

        public string FileImg { get; set; }
        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public Profile Profile { get; set; }

        [BindProperty]
        public string GroupPosition { get; set; }

        [BindProperty]
        public string Sponsor { get; set; }
        public StudyGroupMemeber StudyGroup { get; set; }


        public async Task<IActionResult> OnGetAsync(long id)
        {            
            Profile = await _context.Profiles.FirstOrDefaultAsync(x => x.Id == id);

            return Page();
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
                int imgCount = 0;

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
                            string nameproduct = "News";
                            var uniqueFileName = $"{now.Millisecond}{now.Minute}{now.Second}{now.Day}-".Trim() + nameproduct;

                            var fileExtension = Path.GetExtension(filePath);

                            newFileName = uniqueFileName + fileExtension;

                            // if you wish to save file path to db use this filepath variable + newFileName
                            var fileDbPathName = $"/News/".Trim();

                            filePath = $"{_hostingEnv.WebRootPath}{fileDbPathName}".Trim();

                            if (!(Directory.Exists(filePath)))
                                Directory.CreateDirectory(filePath);

                            var fileName = "";
                            fileName = filePath + $"{newFileName}".Trim();
                            //
                            
                            var fullPath = filePath + Profile.AboutProfile;

                            if (System.IO.File.Exists(fullPath))
                            {
                                System.IO.File.Delete(fullPath);
                            }


                            using (FileStream fsa = System.IO.File.Create(fileName))
                            {
                                file.CopyTo(fsa);
                                fsa.Flush();
                            }



                            pro.AboutProfile = $"{fileDbPathName}{newFileName}";


                            #region Save Image Propertie to Db

                            #endregion

                            if (imgCount >= 5)
                                break;
                        }
                    }
                }


                pro.Sponsor = Sponsor;
                _context.Attach(pro).State = EntityState.Modified;

                await _context.SaveChangesAsync();
                TempData["alert"] = "Profile Updated Successfull";
                return RedirectToPage("./Index");
            }
            catch (Exception c)
            {
                TempData["alert"] = "Unable to Updated Successfull";
            }
            return RedirectToPage("./ChangeImage");
        }

    }

}
