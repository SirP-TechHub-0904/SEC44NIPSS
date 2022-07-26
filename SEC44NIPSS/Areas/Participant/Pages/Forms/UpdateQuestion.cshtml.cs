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
using Microsoft.EntityFrameworkCore;
using SEC44NIPSS.Data.Model;

namespace SEC44NIPSS.Areas.Participant.Pages.Forms
{

    [Authorize]
    public class UpdateQuestionModel : PageModel
    {
        private readonly SEC44NIPSS.Data.NIPSSDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IHostingEnvironment _hostingEnv;
        private int imgCount;

        public UpdateQuestionModel(SEC44NIPSS.Data.NIPSSDbContext context, UserManager<IdentityUser> userManager, IHostingEnvironment hostingEnv)
        {
            _context = context;
            _userManager = userManager;
            _hostingEnv = hostingEnv;
        }
        [BindProperty]
        public Question Question { get; set; }

        [BindProperty]
        public Option Option { get; set; }


        [BindProperty]
        public string siq { get; set; }

        [BindProperty]
        public string liq { get; set; }
        public async Task<IActionResult> OnGetAsync(long id)
        {
            Question = await _context.Questions.Include(x => x.Questionner).FirstOrDefaultAsync(x => x.Id == id);
            Option = await _context.Options.FirstOrDefaultAsync(x => x.QuestionId == id);

            siq = Question.Questionner.ShortLink;
            liq = Question.Questionner.LongLink;
            return Page();
        }

        [BindProperty]
        public string OptionsChoose { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
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
                        string nameproduct = "QuestionPhoto";
                        var uniqueFileName = $"{now.Millisecond}{now.Minute}{now.Second}{now.Day}-".Trim() + nameproduct;

                        var fileExtension = Path.GetExtension(filePath);

                        newFileName = uniqueFileName + fileExtension;

                        // if you wish to save file path to db use this filepath variable + newFileName
                        var fileDbPathName = $"/QuestionPhoto/".Trim();

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
                        var xfilePath = $"{_hostingEnv.WebRootPath}".Trim();
                        string xfullPath = xfilePath + Question.ImageUrl;
                        if (System.IO.File.Exists(xfullPath))
                        {
                            System.IO.File.Delete(xfullPath);
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

                        Question.ImageUrl = $"{fileDbPathName}{newFileNamex}";

                        if (imgCount >= 5)
                            break;
                    }
                }
            }
            _context.Attach(Question).State = EntityState.Modified;

            if (OptionsChoose == "YesNoOption")
            {
                Option.OptionType = OptionType.YesNo;
            }
            else if (OptionsChoose == "LongNoteOption")
            {
                Option.OptionType = OptionType.LongNote;
            }
            else if (OptionsChoose == "ShortNoteOption")
            {
                Option.OptionType = OptionType.ShortNote;
            }
            else if (OptionsChoose == "FiveOption")
            {
                Option.OptionType = OptionType.FiveOption;
            }
            else if (OptionsChoose == "FourOption")
            {
                Option.OptionType = OptionType.FourOption;
            }
            else if (OptionsChoose == "MultipleOption")
            {
                Option.OptionType = OptionType.MultipleOption;
            }
            else if (OptionsChoose == "TableOption")
            {
                Option.OptionType = OptionType.TableOption;
            }


            _context.Attach(Option).State = EntityState.Modified;

            // var xoption = await _context.Options.FirstOrDefaultAsync(x => x.QuestionId == Question.Id);


            await _context.SaveChangesAsync();

            TempData["Updated"] = "Question No." + Question.Number + " has been Updated";

            return RedirectToPage("./UpdateQuestion", new { id = Question.Id });
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
