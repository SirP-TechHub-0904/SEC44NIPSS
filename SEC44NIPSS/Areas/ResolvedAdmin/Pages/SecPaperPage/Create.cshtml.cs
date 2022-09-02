using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using SEC44NIPSS.Data;
using SEC44NIPSS.Data.Model;

namespace SEC44NIPSS.Areas.ResolvedAdmin.Pages.SecPaperPage
{
    public class CreateModel : PageModel
    {
        private readonly SEC44NIPSS.Data.NIPSSDbContext _context;
        private readonly IHostingEnvironment _hostingEnv;

        public CreateModel(SEC44NIPSS.Data.NIPSSDbContext context, IHostingEnvironment hostingEnv)
        {
            _context = context;
            _hostingEnv = hostingEnv;
        }

        public async Task<IActionResult> OnGet(long id)
        {

            var al = await _context.Alumnis.FindAsync(id);
            if (al == null)
            {
                return NotFound();
            }
          
            TempData["title"] = al.Title;
            TempData["id"] = al.Id;
        ViewData["DocumentCategoryId"] = new SelectList(_context.DocumentCategories, "Id", "Title");
            return Page();
        }

        [BindProperty]
        public SecPaper SecPaper { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            string powerpoint = "";
            string report = "";
            string script = "";
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
                        string xname = Regex.Replace(SecPaper.Title, @"[^A-Za-z]", "-"); 
                        var uniqueFileName = $"{now.Millisecond}{now.Minute}{now.Second}{now.Day}-".Trim() + "SEC-Paper";
                        if (file.Name == "Powerpoint")
                        {
                            uniqueFileName = uniqueFileName + "-Powerpoint";

                        }
                        else if (file.Name == "Report")
                        {
                            uniqueFileName = uniqueFileName + "-Report";

                        }
                        else if (file.Name == "Script")
                        {
                            uniqueFileName = uniqueFileName + "-Script";

                        }
                        var fileExtension = Path.GetExtension(filePath);

                        newFileName = uniqueFileName + fileExtension;

                        // if you wish to save file path to db use this filepath variable + newFileName
                        var fileDbPathName = $"/Document2022/".Trim();

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

                        if (file.Name == "Powerpoint")
                        {
                            powerpoint = $"{fileDbPathName}{newFileName}";
                        }
                        else if (file.Name == "Report")
                        {
                            report = $"{fileDbPathName}{newFileName}";
                        }
                        else if (file.Name == "Script")
                        {
                            script = $"{fileDbPathName}{newFileName}";
                        }

                        #region Save Image Propertie to Db

                        #endregion

                        if (imgCount >= 5)
                            break;
                    }
                }
            }


            SecPaper.Powerpoint = powerpoint;
            SecPaper.Script = script;
            SecPaper.Report = report;


            _context.SecPapers.Add(SecPaper);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Main/Details", new { id = SecPaper.AlumniId });
        }
    }
}
