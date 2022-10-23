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
using Microsoft.EntityFrameworkCore;
using SEC44NIPSS.Data;
using SEC44NIPSS.Data.Model;

namespace SEC44NIPSS.Areas.Root.Pages.SecPaperPage
{
    public class EditModel : PageModel
    {
        private readonly SEC44NIPSS.Data.NIPSSDbContext _context;
        private readonly IHostingEnvironment _hostingEnv;

        public EditModel(SEC44NIPSS.Data.NIPSSDbContext context, IHostingEnvironment hostingEnv)
        {
            _context = context;
            _hostingEnv = hostingEnv;
        }

        [BindProperty]
        public SecPaper SecPaper { get; set; }

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            SecPaper = await _context.SecPapers
                .Include(s => s.Alumni)
                .Include(s => s.DocumentCategory).FirstOrDefaultAsync(m => m.Id == id);

            if (SecPaper == null)
            {
                return NotFound();
            }
           ViewData["AlumniId"] = new SelectList(_context.Alumnis, "Id", "Title");
           ViewData["DocumentCategoryId"] = new SelectList(_context.DocumentCategories, "Id", "Title");
            return Page();
        }

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
                        string filePathp = $"{_hostingEnv.WebRootPath}".Trim();

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
                            //remove old one
                            var fullPath = filePathp + SecPaper.Powerpoint;

                            if (System.IO.File.Exists(fullPath))
                            {
                                System.IO.File.Delete(fullPath);
                            }
                            powerpoint = $"{fileDbPathName}{newFileName}";
                        }
                        else if (file.Name == "Report")
                        {
                            //remove old one
                            var fullPath = filePathp + SecPaper.Report;

                            if (System.IO.File.Exists(fullPath))
                            {
                                System.IO.File.Delete(fullPath);
                            }
                            report = $"{fileDbPathName}{newFileName}";
                        }
                        else if (file.Name == "Script")
                        {
                            //remove old one
                            var fullPath = filePathp + SecPaper.Script;

                            if (System.IO.File.Exists(fullPath))
                            {
                                System.IO.File.Delete(fullPath);
                            }
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

            _context.Attach(SecPaper).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SecPaperExists(SecPaper.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("/Main/Details", new { id = SecPaper.AlumniId });
        }

        private bool SecPaperExists(long id)
        {
            return _context.SecPapers.Any(e => e.Id == id);
        }
    }
}
