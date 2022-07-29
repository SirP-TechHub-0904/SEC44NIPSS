using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
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

namespace SEC44NIPSS.Areas.Admin.Pages.ProfileAccount
{
    [Authorize(Roles = "mSuperAdmin,Admin")]

    public class EditModel : PageModel
    {
        private readonly SEC44NIPSS.Data.NIPSSDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IHostingEnvironment _hostingEnv;


        public EditModel(SEC44NIPSS.Data.NIPSSDbContext context, UserManager<IdentityUser> userManager, IHostingEnvironment hostingEnv)
        {
            _context = context;
            _userManager = userManager;
            _hostingEnv = hostingEnv;
        }

        [BindProperty]
        public Profile Profile { get; set; }

        [BindProperty]
        public string Email { get; set; }

        [BindProperty]
        public long StudyGroupId { get; set; }


        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Profile = await _context.Profiles
                .Include(p => p.User)
                .Include(x => x.StudyGroupMemeber)
                                           .ThenInclude(x => x.StudyGroup)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (Profile == null)
            {
                return NotFound();
            }
            ViewData["AlumniId"] = new SelectList(_context.Alumnis, "Id", "Title");
            ViewData["StudyGroupId"] = new SelectList(_context.StudyGroups, "Id", "Title");

            ViewData["UserId"] = new SelectList(_context.Users.OrderByDescending(x => x.Email), "Id", "Email");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
          
            int imgCount = 0;
           // var fullPath = string.Empty;
            if (HttpContext.Request.Form.Files != null && HttpContext.Request.Form.Files.Count > 0)
            {
                var newFileName = string.Empty;
                var newFileNameThumbnail = string.Empty;
                var filePath = string.Empty;
                var LargefilePath = string.Empty;
                var filePathThumbnail = string.Empty;
                string pathdb = string.Empty;

                var files = HttpContext.Request.Form.Files;
                foreach (var file in files)
                {

                    if (file.Length > 0)
                    {
                        filePath = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                        LargefilePath = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                        filePathThumbnail = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                        imgCount++;
                        var now = DateTime.Now;
                        string nameproduct = "ProfileImage";
                        var uniqueFileName = $"{now.Millisecond}{now.Minute}{now.Second}{now.Day}-".Trim() + nameproduct;

                        var fileExtension = Path.GetExtension(filePath);

                        newFileName = uniqueFileName + fileExtension;

                        // if you wish to save file path to db use this filepath variable + newFileName
                        var fileDbPathName = $"/ProfileImage/".Trim();

                        filePath = $"{_hostingEnv.WebRootPath}{fileDbPathName}".Trim();

                        var LagefileDbPathName = $"/GalleryLargeImage/".Trim();

                        LargefilePath = $"{_hostingEnv.WebRootPath}{LagefileDbPathName}".Trim();

                        if (!(Directory.Exists(filePath)))
                            Directory.CreateDirectory(filePath);

                        if (!(Directory.Exists(LargefilePath)))
                            Directory.CreateDirectory(LargefilePath);

                        var fileName = "";
                        var LargefileName = "";
                        fileName = filePath + $"{newFileName}".Trim();
                        LargefileName = LargefilePath + $"{newFileName}".Trim();


                        string newFileNamex = uniqueFileName + fileExtension;

                        //
                       // var fileName = "";
                        fileName = filePath + $"{newFileName}".Trim();

                        var oldfilePath = $"{_hostingEnv.WebRootPath}".Trim();
                        string fullPath = filePath + Profile.AboutProfile;
                        if (System.IO.File.Exists(fullPath))
                        {
                            System.IO.File.Delete(fullPath);
                        }
                        using (FileStream fsa = System.IO.File.Create(LargefileName))
                        {
                            file.CopyTo(fsa);
                            fsa.Flush();
                            fsa.Close();
                            fsa.Dispose();
                        }

                        Image myImage = Image.FromFile(LargefileName, true);
                        myImage = ScaleByPercent(myImage, 60);
                        myImage.Save(filePath + newFileNamex, ImageFormat.Jpeg);

                        myImage.Dispose();

                        Profile.AboutProfile = $"{fileDbPathName}{newFileNamex}";


                        fullPath = fileName;



                        if (imgCount >= 5)
                            break;
                    }
                }
            }



            _context.Attach(Profile).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProfileExists(Profile.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }


            try
            {
                if (!String.IsNullOrEmpty(Email))
                {
                    var user = await _userManager.FindByIdAsync(Profile.UserId);
                    var xuser = await _userManager.GenerateChangeEmailTokenAsync(user, Email);
                    var chemail = await _userManager.ChangeEmailAsync(user, Email, xuser);
                    if (chemail.Succeeded)
                    {
                        TempData["er"] = "Email Changed";

                    }
                    else
                    {
                        return Page();
                    }
                }
            }
            catch (Exception c)
            {

            }

            try
            {
                if (StudyGroupId > 0)
                {
                    var m = await _context.StudyGroupMemebers.FirstOrDefaultAsync(x => x.ProfileId == Profile.Id);
                    if(m == null)
                    {
                        StudyGroupMemeber sd = new StudyGroupMemeber();
                        sd.StudyGroupId = StudyGroupId;
                        sd.ProfileId = Profile.Id;
                        sd.Position = Profile.Position;
                        _context.StudyGroupMemebers.Add(sd);
                        await _context.SaveChangesAsync();
                        TempData["errr"] = "Group updated";
                    }
                    else{
                        m.StudyGroupId = StudyGroupId;
                        _context.Attach(Profile).State = EntityState.Modified;
                        await _context.SaveChangesAsync();

                        TempData["errr"] = "Group Changed";
                    }
                }
            }
            catch (Exception d)
            {

            }
            TempData["err"] = "Account Updated";
            return RedirectToPage("./Result");
        }

        private bool ProfileExists(long id)
        {
            return _context.Profiles.Any(e => e.Id == id);
        }

        static Image ScaleByPercent(Image imgPhoto, int Percent)
        {
            float nPercent = ((float)Percent / 100);

            int sourceWidth = imgPhoto.Width;
            int sourceHeight = imgPhoto.Height;
            int sourceX = 0;
            int sourceY = 0;

            int destX = 0;
            int destY = 0;
            int destWidth = (int)(sourceWidth * nPercent);
            int destHeight = (int)(sourceHeight * nPercent);

            Bitmap bmPhoto = new Bitmap(destWidth, destHeight,
                                     PixelFormat.Format24bppRgb);
            bmPhoto.SetResolution(imgPhoto.HorizontalResolution,
                                    imgPhoto.VerticalResolution);

            Graphics grPhoto = Graphics.FromImage(bmPhoto);
            grPhoto.InterpolationMode = InterpolationMode.HighQualityBicubic;

            grPhoto.DrawImage(imgPhoto,
                new Rectangle(destX, destY, destWidth, destHeight),
                new Rectangle(sourceX, sourceY, sourceWidth, sourceHeight),
                GraphicsUnit.Pixel);

            grPhoto.Dispose();
            return bmPhoto;
        }
        /// <summary> 
        /// Saves an image as a jpeg image, with the given quality 
        /// </summary> 
        /// <param name="path"> Path to which the image would be saved. </param> 
        /// <param name="quality"> An integer from 0 to 100, with 100 being the highest quality. </param> 
        public static void SaveJpeg(string path, Image img, int quality)
        {
            if (quality < 0 || quality > 100)
                throw new ArgumentOutOfRangeException("quality must be between 0 and 100.");

            // Encoder parameter for image quality 
            EncoderParameter qualityParam = new EncoderParameter(Encoder.Quality, quality);
            // JPEG image codec 
            ImageCodecInfo jpegCodec = GetEncoderInfo("image/jpeg");
            EncoderParameters encoderParams = new EncoderParameters(1);
            encoderParams.Param[0] = qualityParam;
            img.Save(path, jpegCodec, encoderParams);
        }

        /// <summary> 
        /// Returns the image codec with the given mime type 
        /// </summary> 
        private static ImageCodecInfo GetEncoderInfo(string mimeType)
        {
            // Get image codecs for all image formats 
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();

            // Find the correct image codec 
            for (int i = 0; i < codecs.Length; i++)
                if (codecs[i].MimeType == mimeType)
                    return codecs[i];

            return null;
        }

    }
}
