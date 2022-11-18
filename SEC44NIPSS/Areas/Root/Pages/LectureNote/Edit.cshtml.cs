using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SEC44NIPSS.Data;
using SEC44NIPSS.Data.Model;

namespace SEC44NIPSS.Areas.Root.Pages.LectureNote
{
    [Authorize]
    public class EditModel : PageModel
    {
        private readonly SEC44NIPSS.Data.NIPSSDbContext _context;
        private readonly IHostingEnvironment _hostingEnv;
        private readonly ILogger<EditModel> _logger;

        public EditModel(SEC44NIPSS.Data.NIPSSDbContext context, IHostingEnvironment hostingEnv, ILogger<EditModel> logger)
        {
            _context = context;
            _hostingEnv = hostingEnv;
            _logger = logger;
        }

        [BindProperty]
        public Event Event { get; set; }

        [BindProperty]
        public Document Document { get; set; }
        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Event = await _context.Events.Include(x => x.Document).AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);

            if (Event == null)
            {
                return NotFound();
            }
            ViewData["DocumentCategoryId"] = new SelectList(_context.DocumentCategories, "Id", "Title");
            ViewData["ProfileId"] = new SelectList(_context.Profiles, "Id", "FullName");
            if (Event.Document != null)
            {
                if (Event.Document.EventId != null)
                {
                    Document = await _context.Documents.Include(x => x.Event).FirstOrDefaultAsync(m => m.EventId == Event.Document.EventId);
                }
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            int imgCount = 0;
            string filedoc = "";
            string filedocCover = "";

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
                            filedoc = $"{fileDbPathName}{newFileName}";
                        }
                        else if (file.Name == "CoverImage")
                        {
                            filedocCover = $"{fileDbPathName}{newFileName}";
                        }
                        #region Save Image Propertie to Db

                        #endregion

                        if (imgCount >= 5)
                            break;
                    }
                }
            }

            if (!String.IsNullOrEmpty(filedoc))
            {
                 Document.FileName = filedoc;
            }
            if (!String.IsNullOrEmpty(filedocCover))
            {
                  Document.CoverImage = filedocCover;

            }
            //
            var xEvent = await _context.Events.Include(x => x.Document).AsNoTracking().FirstOrDefaultAsync(m => m.Id == Event.Id);
            if (xEvent.Document != null)
            {
                if (xEvent.Document.EventId != null)
                {
                    var hdoc = await _context.Documents.AsNoTracking().FirstOrDefaultAsync(x => x.EventId == Event.Id);
                    hdoc.DocumentCategoryId = Document.DocumentCategoryId;
                    hdoc.ProfileId = Document.ProfileId;
                    hdoc.Title = Event.Title;
                    hdoc.CoverImage = Document.CoverImage;
                    hdoc.FileName = Document.FileName;
                    hdoc.DocType = Document.DocType;
                    _context.Attach(hdoc).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                }
            }
            else
            {
                Document.EventId = Event.Id;
                _context.Documents.Add(Document);
                //await _context.SaveChangesAsync();
            } 
            _context.Attach(Event).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EventExists(Event.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool EventExists(long id)
        {
            return _context.Events.Any(e => e.Id == id);
        }
    }
}
