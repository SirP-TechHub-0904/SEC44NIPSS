﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SEC44NIPSS.Data;
using SEC44NIPSS.Data.Model;

namespace SEC44NIPSS.Areas.Participant.Pages.ParlyPage
{
    [Authorize(Roles = "Participant")]

    public class UploadModel : PageModel
    {
        private readonly SEC44NIPSS.Data.NIPSSDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IHostingEnvironment _hostingEnv;

        public UploadModel(SEC44NIPSS.Data.NIPSSDbContext context, UserManager<IdentityUser> userManager, IHostingEnvironment hostingEnv)
        {
            _context = context;
            _userManager = userManager;
            _hostingEnv = hostingEnv;
        }

        public async Task<IActionResult> OnGetAsync(long? id, string redir)
        {
            if (id == null)
            {
                return NotFound();
            }

            ParlyReportCategories = await _context.ParlyReportCategories.Include(x=>x.ParlyReportDocuments).FirstOrDefaultAsync(x=>x.Id == id);
           
            if(ParlyReportCategories == null)
            {
                return RedirectToPage("/Index", new { area = "" });
            }
            Redirt = redir;
            return Page();
        }
        [BindProperty]
        public string Redirt { get; set; }

        [BindProperty]
        public ParlyReportDocument ParlyReportDocument { get; set; }

        [BindProperty]
        public ParlyReportCategory ParlyReportCategories { get; set; }

        [BindProperty]
        public Profile Profile { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                TempData["usernull"] = "null";
            }
            else
            {
                Profile = await _context.Profiles.FirstOrDefaultAsync(x => x.UserId == user.Id);

            }
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
                        
                        var uniqueFileName = $"{now.Millisecond}{now.Minute}{now.Second}{now.Day}-".Trim() + "Parly-Contribution";
                       
                        var fileExtension = Path.GetExtension(filePath);

                        newFileName = uniqueFileName + fileExtension;

                        // if you wish to save file path to db use this filepath variable + newFileName
                        var fileDbPathName = $"/ParlyDocument2022/".Trim();

                        filePath = $"{_hostingEnv.WebRootPath}{fileDbPathName}".Trim();

                        if (!(Directory.Exists(filePath)))
                            Directory.CreateDirectory(filePath);

                        var fileName = "";
                        fileName = filePath + $"{newFileName}".Trim();

                        // copy the file to the desired location from the tempMemoryLocation of IFile and flush temp memory
                        using (FileStream fs = System.IO.File.Create(fileName))
                        {
                            file.CopyTo(fs);
                            fs.Flush();
                        }

                          ParlyReportDocument.Document = $"{fileDbPathName}{newFileName}";
                       

                        #region Save Image Propertie to Db

                        #endregion

                        if (imgCount >= 5)
                            break;
                    }
                }
            }



            ParlyReportDocument.ProfileId = Profile.Id;
            ParlyReportDocument.Date = DateTime.UtcNow.AddHours(1);
            _context.ParlyReportDocuments.Add(ParlyReportDocument);
            await _context.SaveChangesAsync();

            TempData["result"] = "Uploaded Successfully";
            if (!String.IsNullOrEmpty(Redirt))
            {
                return RedirectToPage("./Documents", new { id = ParlyReportDocument.ParlyReportCategoryId });
            }
            return RedirectToPage("./Index");
        }
    }
}
