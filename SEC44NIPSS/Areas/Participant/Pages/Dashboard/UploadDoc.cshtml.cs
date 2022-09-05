﻿using System;
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
using SEC44NIPSS.Data;
using SEC44NIPSS.Data.Model;

namespace SEC44NIPSS.Areas.Participant.Pages.Dashboard
{

    public class EventList
    {
        public long Id { get; set; }
        public string Title { get; set; }
    }
    public class UploadDocModel : PageModel
    {
        private readonly SEC44NIPSS.Data.NIPSSDbContext _context;
        private readonly IHostingEnvironment _hostingEnv;
        private readonly UserManager<IdentityUser> _userManager;


        public UploadDocModel(SEC44NIPSS.Data.NIPSSDbContext context, IHostingEnvironment hostingEnv, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _hostingEnv = hostingEnv;
            _userManager = userManager;
        }

        public DocumentCategory DocumentCategory { get; set; }
        public long? CID { get; set; }
        public async Task<IActionResult> OnGetAsync(long id)
        {

            DocumentCategory = await _context.DocumentCategories.FirstOrDefaultAsync(x => x.Id == id);
            if(DocumentCategory == null)
            {
                return NotFound();
            }
            CID = id;
            return Page();
        }

        [BindProperty]
        public Document Document { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
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
                        string nameproduct = "Doc";
                        var uniqueFileName = $"{now.Millisecond}{now.Minute}{now.Second}{now.Day}-".Trim() + nameproduct;

                        var fileExtension = Path.GetExtension(filePath);

                        newFileName = uniqueFileName + fileExtension;

                        // if you wish to save file path to db use this filepath variable + newFileName
                        var fileDbPathName = $"/DOCUMENTS/".Trim();

                        filePath = $"{_hostingEnv.WebRootPath}{fileDbPathName}".Trim();

                        if (!(Directory.Exists(filePath)))
                            Directory.CreateDirectory(filePath);


                        var fileName = "";
                        fileName = filePath + $"{newFileName}".Trim();
                        //

                        var fullPath = filePath + Document.FileName;

                        if (System.IO.File.Exists(fullPath))
                        {
                            System.IO.File.Delete(fullPath);
                        }


                        using (FileStream fsa = System.IO.File.Create(fileName))
                        {
                            file.CopyTo(fsa);
                            fsa.Flush();
                        }



                        

                        if (file.Name == "filesPhoto")
                        {
                            Document.FileName = $"{fileDbPathName}{newFileName}";
                        }
                        else if (file.Name == "CoverImage")
                        {
                            Document.CoverImage = $"{fileDbPathName}{newFileName}";
                        }
                        #region Save Image Propertie to Db

                        #endregion

                        if (imgCount >= 5)
                            break;
                    }
                }
            }
            //
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
            var Profile = await _context.Profiles.FirstOrDefaultAsync(x => x.UserId == user.Id);

            Document.ProfileId = Profile.Id;

            Document.Date = DateTime.UtcNow.AddHours(1);
            _context.Documents.Add(Document);
            await _context.SaveChangesAsync();

            return RedirectToPage("./DocumentList", new { id = Document.DocumentCategoryId });
        }
    }
}
