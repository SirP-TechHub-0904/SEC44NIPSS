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
    public class FormMakerModel : PageModel
    {
        private readonly SEC44NIPSS.Data.NIPSSDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IHostingEnvironment _hostingEnv;
        private int imgCount;

        public FormMakerModel(SEC44NIPSS.Data.NIPSSDbContext context, UserManager<IdentityUser> userManager, IHostingEnvironment hostingEnv)
        {
            _context = context;
            _userManager = userManager;
            _hostingEnv = hostingEnv;
        }

        public Questionner QuestionnerList { get; set; }

        public async Task<IActionResult> OnGetAsync(string o = null, string q = null)
        {
            var user = await _userManager.GetUserAsync(User);
            var profile = await _context.Profiles.FirstOrDefaultAsync(x => x.UserId == user.Id);
            if (o != null)
            {
                QuestionnerList = await _context.Questionners.Include(x => x.Questions).ThenInclude(x => x.Options).FirstOrDefaultAsync(x => x.ShortLink == o);
            }
            else
            {
                return RedirectToPage("./Index");
            }
            if (QuestionnerList == null)
            {
                return RedirectToPage("./Index");
            }
            QuestionnerId = QuestionnerList.Id;
                siq = QuestionnerList.ShortLink;
                liq = QuestionnerList.LongLink;
            
            return Page();
        }
        [BindProperty]
        public Option OptionUpdate { get; set; }

        [BindProperty]
        public Question QuestionUpdate { get; set; }

        [BindProperty]
        public Questionner Questionner { get; set; }

        [BindProperty]
        public long ProfileId { get; set; }

        [BindProperty]
        public long QuestionnerId { get; set; }
        [BindProperty]
        public string siq { get; set; }

        [BindProperty]
        public string liq { get; set; }
        [BindProperty]
        public long xQuestionId { get; set; }
        public async Task<IActionResult> OnPostNewQuestion()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var xquestion = await _context.Questions.Where(x => x.QuestionnerId == QuestionnerId).OrderByDescending(x => x.Number).Select(x => x.Number).ToListAsync();

            int xc = xquestion.FirstOrDefault() + 1;
            Question q = new Question();
            q.QuestionnerId = QuestionnerId;
            q.Title = "Question XXXX " + xc;
            q.Number = xquestion.FirstOrDefault() + 1;

            _context.Questions.Add(q);
            await _context.SaveChangesAsync();

            Option o = new Option();
            o.QuestionId = q.Id;
            o.OptionType = OptionType.FourOption;
            o.OptionList1 = "Option one";
            o.OptionList2 = "Option two";
            o.OptionList3 = "Option three";
            o.OptionList4 = "Option four";

            _context.Options.Add(o);
            await _context.SaveChangesAsync();
            var qx = await _context.Questionners.Include(x => x.Questions).ThenInclude(x => x.Options).FirstOrDefaultAsync(x => x.Id == QuestionnerId);
            TempData["Updated"] = "New of Question No." + q.Number + " was created successful";

            return RedirectToPage("./FormMaker", new { o = qx.ShortLink, q = qx.LongLink });
        }


        public async Task<IActionResult> OnPostDuplicate()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var xquestion = await _context.Questions.Include(x => x.Options).FirstOrDefaultAsync(x => x.Id == xQuestionId);



            Question q = new Question();
            q.QuestionnerId = xquestion.QuestionnerId;
            q.Title = xquestion.Title;
            q.Number = xquestion.Number + 1;

            if (!String.IsNullOrEmpty(xquestion.ImageUrl))
            {
                var xfilePath = $"{_hostingEnv.WebRootPath}".Trim();
                string xfullPath = xfilePath + xquestion.ImageUrl;

                var fileDbPathName = $"/QuestionPhoto/".Trim();
                var now = DateTime.Now;
                string nameproduct = "QuestionPhoto";
                string filePath = $"{_hostingEnv.WebRootPath}{fileDbPathName}".Trim();
                var newFileNamex = $"{now.Millisecond}{now.Minute}{now.Second}{now.Day}-".Trim() + nameproduct + ".jpeg";

                Image myImage = Image.FromFile(xfullPath, true);
                //myImage = ScaleByPercent(myImage, 60);
                myImage.Save(filePath + newFileNamex);
                myImage.Dispose();
                q.ImageUrl = $"{fileDbPathName}{newFileNamex}";
            }
            _context.Questions.Add(q);
            await _context.SaveChangesAsync();

            var xoption = await _context.Options.FirstOrDefaultAsync(x => x.QuestionId == xquestion.Id);
            Option o = new Option();
            o.QuestionId = q.Id;
            o.OptionType = xoption.OptionType;
            o.LongNoteMaximumLength = xoption.LongNoteMaximumLength;
            o.LongNoteMinimumLength = xoption.LongNoteMinimumLength;
            o.ShortNoteMaximumLength = xoption.ShortNoteMaximumLength;
            o.ShortNoteMinimumLength = xoption.ShortNoteMinimumLength;
            o.MultipleOption1 = xoption.MultipleOption1;
            o.MultipleOption2 = xoption.MultipleOption2;
            o.MultipleOption3 = xoption.MultipleOption3;
            o.MultipleOption4 = xoption.MultipleOption4;
            o.MultipleOption5 = xoption.MultipleOption5;
            o.MultipleOption6 = xoption.MultipleOption6;
            o.MultipleOption7 = xoption.MultipleOption7;
            o.No = xoption.No;
            o.Yes = xoption.Yes;
            o.OptionList1 = xoption.OptionList1;
            o.OptionList2 = xoption.OptionList2;
            o.OptionList3 = xoption.OptionList3;
            o.OptionList4 = xoption.OptionList4;
            o.OptionList5 = xoption.OptionList5;

            _context.Options.Add(o);
            await _context.SaveChangesAsync();
            var qx = await _context.Questionners.Include(x => x.Questions).ThenInclude(x => x.Options).FirstOrDefaultAsync(x => x.Id == xquestion.QuestionnerId);

            int sn = qx.Questions.Where(x => x.Number >= q.Number).OrderBy(x=>x.Number).FirstOrDefault().Number;
            foreach (var x in qx.Questions.Where(x => x.Number >= q.Number).OrderBy(x => x.Number).Skip(1))
            {
                sn++;
                x.Number = sn;
                _context.Attach(x).State = EntityState.Modified;

            }
            await _context.SaveChangesAsync();

            TempData["Updated"] = "Duplicate of Question No." + xquestion.Number + " was duplicated successful";
            return RedirectToPage("./FormMaker", new { o = qx.ShortLink, q = qx.LongLink });
        }
        public async Task<IActionResult> OnPostArrowDown()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            int nxt = 0;
            var xquestion = await _context.Questions.Include(x => x.Options).FirstOrDefaultAsync(x => x.Id == xQuestionId);
            {
                nxt = xquestion.Number;
                xquestion.Number = xquestion.Number + 1;

                _context.Attach(xquestion).State = EntityState.Modified;
                int cnxt = nxt + 1;
                var ixquestion = await _context.Questions.Include(x => x.Options).FirstOrDefaultAsync(x => x.Number == cnxt && x.QuestionnerId == xquestion.QuestionnerId);
                {
                   
                    ixquestion.Number = ixquestion.Number - 1;

                    _context.Attach(ixquestion).State = EntityState.Modified;


                }
            }
            await _context.SaveChangesAsync();

            var qx = await _context.Questionners.Include(x => x.Questions).ThenInclude(x => x.Options).FirstOrDefaultAsync(x => x.Id == xquestion.QuestionnerId);
         
            TempData["Updated"] = "Question No." + xquestion.Number + " was moved down successful";
            return RedirectToPage("./FormMaker", new { o = qx.ShortLink, q = qx.LongLink });
        }


        public async Task<IActionResult> OnPostArrowUp()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var xquestion = await _context.Questions.Include(x => x.Options).FirstOrDefaultAsync(x => x.Id == xQuestionId);
            {

                xquestion.Number = xquestion.Number-1;
                if(xquestion.Number == 0)
                {
                    xquestion.Number = 1;
                }
                _context.Attach(xquestion).State = EntityState.Modified;

            }
            await _context.SaveChangesAsync();

            var qx = await _context.Questionners.Include(x => x.Questions).ThenInclude(x => x.Options).FirstOrDefaultAsync(x => x.Id == xquestion.QuestionnerId);
            int n = xquestion.Number;
            //int sn = qx.Questions.Where(x => x.Number >= q.Number).OrderBy(x => x.Number).FirstOrDefault().Number;
            foreach (var x in qx.Questions.Where(x => x.Number >= n).OrderBy(x => x.Number).Skip(1))
            {
                n++;
                x.Number = n;
                _context.Attach(x).State = EntityState.Modified;

            }
            await _context.SaveChangesAsync();

            TempData["Updated"] = "Question No." + xquestion.Number + " was moved up successful";
            return RedirectToPage("./FormMaker", new { o = qx.ShortLink, q = qx.LongLink });
        }
        public async Task<IActionResult> OnPostResetNumber()
        {
           

            var qx = await _context.Questionners.Include(x => x.Questions).ThenInclude(x => x.Options).FirstOrDefaultAsync(x => x.Id == QuestionnerId);
            int n = 0;
            //int sn = qx.Questions.Where(x => x.Number >= n).OrderBy(x => x.Number).FirstOrDefault().Number;
            foreach (var x in qx.Questions.OrderBy(x => x.Number))
            {
                n++;
                x.Number = n;
                _context.Attach(x).State = EntityState.Modified;

            }
            await _context.SaveChangesAsync();


            TempData["Updated"] = "Numbering Reset Successfully";
            return RedirectToPage("./FormMaker", new { o = siq, q = liq });
        }



        public async Task<IActionResult> OnPostDelete()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            
            var xquestion = await _context.Questions.Include(x => x.Options).FirstOrDefaultAsync(x => x.Id == xQuestionId);
            int n = xquestion.Number;
            if (xquestion != null)
            {
                _context.Questions.Remove(xquestion);
                await _context.SaveChangesAsync();
            }
            await _context.SaveChangesAsync();

            var qx = await _context.Questionners.Include(x => x.Questions).ThenInclude(x => x.Options).FirstOrDefaultAsync(x => x.Id == xquestion.QuestionnerId);
            n = n - 1;
            foreach (var x in qx.Questions.Where(x => x.Number > n).OrderBy(x => x.Number))
            {
                n++;
                x.Number = n;
                _context.Attach(x).State = EntityState.Modified;

            }
            await _context.SaveChangesAsync();


            TempData["Updated"] = "Deleted Successfully";
            return RedirectToPage("./FormMaker", new { o = siq, q = liq });
        }
        
             public async Task<IActionResult> OnPostRessetLinks()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var xquestionner = await _context.Questionners.FirstOrDefaultAsync(x => x.Id == QuestionnerId);

            xquestionner.ShortLink = CreateD();
            string code = Guid.NewGuid().ToString();
            string code1 = Guid.NewGuid().ToString();
            string code2 = Guid.NewGuid().ToString();
            xquestionner.LongLink = code + CreateD() + code1 + code2;
            _context.Attach(xquestionner).State = EntityState.Modified;

            await _context.SaveChangesAsync();
            TempData["Updated"] = "New Links have been Created Successfully";
            return RedirectToPage("./FormMaker", new { o = xquestionner.ShortLink, q = xquestionner.LongLink });
        }

        public async Task<IActionResult> OnPostRequired()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var xquestion = await _context.Questions.Include(x => x.Options).FirstOrDefaultAsync(x => x.Id == xQuestionId);

            if (String.IsNullOrEmpty(xquestion.Required))
            {
                xquestion.Required = "required";
                TempData["Updated"] = "Question No." + xquestion.Number + " is required";
            }
            else
            {
                xquestion.Required = "";
                TempData["Updated"] = "Question No." + xquestion.Number + " is not required";

            }
            _context.Attach(xquestion).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return RedirectToPage("./FormMaker", new { o = siq, q = liq });
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
        private static string CreateA(int length = 3)
        {
            // Create a string of characters, numbers, special characters that allowed in the password  
            string validChars = "ABCDEFGHJKLMNOPQRSTUVWXYZ";
            Random random = new Random();

            // Select one random character at a time from the string  
            // and create an array of chars  
            char[] chars = new char[length];
            for (int i = 0; i < length; i++)
            {
                chars[i] = validChars[random.Next(0, validChars.Length)];
            }
            return new string(chars);
        }
        private static string CreateB(int length = 3)
        {
            // Create a string of characters, numbers, special characters that allowed in the password  
            string validChars = "abcdefghijklmnopqrstuvwxyz";
            Random random = new Random();

            // Select one random character at a time from the string  
            // and create an array of chars  
            char[] chars = new char[length];
            for (int i = 0; i < length; i++)
            {
                chars[i] = validChars[random.Next(0, validChars.Length)];
            }
            return new string(chars);
        }
        private static string CreateC(int length = 2)
        {
            // Create a string of characters, numbers, special characters that allowed in the password  
            string validChars = "0123456789";
            Random random = new Random();

            // Select one random character at a time from the string  
            // and create an array of chars  
            char[] chars = new char[length];
            for (int i = 0; i < length; i++)
            {
                chars[i] = validChars[random.Next(0, validChars.Length)];
            }
            return new string(chars);
        }

        private static string CreateD(int length = 8)
        {
            // Create a string of characters, numbers, special characters that allowed in the password  
            string validChars = CreateA() + CreateB() + CreateC();
            Random random = new Random();

            // Select one random character at a time from the string  
            // and create an array of chars  
            char[] chars = new char[length];
            for (int i = 0; i < length; i++)
            {
                chars[i] = validChars[random.Next(0, validChars.Length)];
            }
            return new string(chars);
        }

    }
}
