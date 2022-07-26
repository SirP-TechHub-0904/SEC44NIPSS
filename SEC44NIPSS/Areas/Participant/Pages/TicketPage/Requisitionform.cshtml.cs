using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SEC44NIPSS.Data;
using SEC44NIPSS.Data.Model;

namespace SEC44NIPSS.Areas.Participant.Pages.TicketPage
{
    [Authorize]
    public class RequisitionformModel : PageModel
    {
        private readonly SEC44NIPSS.Data.NIPSSDbContext _context;
        private readonly IHostingEnvironment _hostingEnv;
        private readonly UserManager<IdentityUser> _userManager;


        public RequisitionformModel(SEC44NIPSS.Data.NIPSSDbContext context, IHostingEnvironment hostingEnv, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _hostingEnv = hostingEnv;
            _userManager = userManager;
        }
        public Profile Profile { get; set; }
        public async Task<IActionResult> OnGet()
        {
            ViewData["Category"] = new SelectList(_context.TicketCategories, "Title", "Title");

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                TempData["usernull"] = "null";
            }
            else
            {
                Profile = await _context.Profiles.Include(x => x.MyGallery).Include(x=>x.StudyGroupMemeber).ThenInclude(x=>x.StudyGroup).FirstOrDefaultAsync(x => x.UserId == user.Id);

            }
            return Page();
        }


        public List<SelectListItem> SubList { get; set; }

        public async Task<JsonResult> OnGetSubCat(string id)
        {

            List<TicketSubCategory> lga = new List<TicketSubCategory>();

            var query = await _context.TicketSubCategories.Include(x => x.TicketCategory).Where(x => x.TicketCategory.Title == id).ToListAsync();


            SubList = query.Select(a =>
                                new SelectListItem
                                {
                                    Value = a.Title,
                                    Text = a.Title
                                }).ToList();
            return new JsonResult(SubList);
        }
        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }
            [Required]
            public string Fullname { get; set; }
            [Required]
            public string PhoneNumber { get; set; }
            [Required]
            public string Category { get; set; }
            [Required]
            public string SubCategory { get; set; }
            [Required]
            public string Office { get; set; }
            public string Message { get; set; }
            [Required]
            public string Priority { get; set; }
            public string StudyGroup { get; set; }
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                Ticket Ticket = new Ticket();

                int imgCount = 0;
                var fullPath = string.Empty;
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
                            string nameproduct = "maintenace";
                            var uniqueFileName = $"{now.Millisecond}{now.Minute}{now.Second}{now.Day}-".Trim() + nameproduct;

                            var fileExtension = Path.GetExtension(filePath);

                            newFileName = uniqueFileName + fileExtension;

                            // if you wish to save file path to db use this filepath variable + newFileName
                            var fileDbPathName = $"/Maintenace/".Trim();

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

                            Ticket.Image = $"{fileDbPathName}{newFileNamex}";


                            fullPath = fileName;



                            if (imgCount >= 5)
                                break;
                        }
                    }
                }

                var user = await _userManager.GetUserAsync(User);
                System.Random random = new System.Random();
                var inos = DateTime.UtcNow.ToString("MMddHmmssyyyy");
                Ticket.CreatedTime = DateTime.UtcNow.AddHours(1);
                Ticket.Fullname = Input.Fullname;
                Ticket.Email = Input.Email;
                Ticket.PhoneNumber = Input.PhoneNumber;
                Ticket.Priority = Input.Priority;
                Ticket.Stages = "Submitted";
                Ticket.HouseOfficeNumber = Input.Office;
                Ticket.Details = Input.Message;
                Ticket.UserId = user.Id;
                Ticket.Category = Input.Category;
                Ticket.SubCategory = Input.SubCategory;
                _context.Tickets.Add(Ticket);
                await _context.SaveChangesAsync();
                var cx = await _context.Tickets.FirstOrDefaultAsync(x=>x.Id == Ticket.Id);
                cx.TicketNumber = "A" + Ticket.Id.ToString("0000");
                _context.Attach(cx).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                StreamReader sr = new StreamReader(System.IO.Path.Combine(_hostingEnv.WebRootPath, "emailsec.html"));
                MailMessage mail = new MailMessage();
                string mi = $"Your Request has been booked with Ticket Number (" + Ticket.TicketNumber + ") <br>Priority: " + Ticket.Priority + "<br>" +
                    $"Category: " + Ticket.Category + "<br>" +
                    $"Sub Category: " + Ticket.SubCategory + "<br>" +
                    $"Date and Time: " + Ticket.CreatedTime + "<br>" +
                    $"Phone: " + Ticket.PhoneNumber + "<br>" +
                    $"House/Office Number: " + Ticket.HouseOfficeNumber + "<br><strong>Details</strong><br>" + Ticket.Details + "<br><br>Thanks Your Challenge will be responded to as soon as possible";


                string mailmsg = sr.ReadToEnd();
                mailmsg = mailmsg.Replace("{NAME}", Ticket.Fullname);
                mailmsg = mailmsg.Replace("{TITLE}", "");
                mailmsg = mailmsg.Replace("{BODY}", mi);
                mail.Body = mailmsg;
                sr.Close();

                Message ms = new Message();
                ms.Recipient = Ticket.Email;
                ms.Title = "Work and Maintenance Department";
                ms.Mail = mailmsg;
                ms.Retries = 0; ms.NotificationStatus = NotificationStatus.NotSent; ms.NotificationType = NotificationType.Email;
                _context.Messages.Add(ms);
                await _context.SaveChangesAsync();

                //sms
                Message sms = new Message();
                sms.Recipient = Ticket.PhoneNumber;
                sms.Title = "Work and Maintenance Department";
                sms.Mail = "Your Request has been booked with Ticket Number (" + Ticket.TicketNumber + "). Kindly Check your email or your dashboard for details";
                sms.Retries = 0; sms.NotificationStatus = NotificationStatus.NotSent; sms.NotificationType = NotificationType.SMS;
                _context.Messages.Add(sms);
                await _context.SaveChangesAsync();

                var ticketsupervisor = await _context.TicketSupervisor.ToListAsync();
                try
                {

                   
                    foreach (var mm in ticketsupervisor)
                    {
                        long emx = 0;
                        long ymx = 0;
                        if (mm.SendEmail == true)
                        {
                            StreamReader xsr = new StreamReader(System.IO.Path.Combine(_hostingEnv.WebRootPath, "emailsec.html"));
                            MailMessage xmail = new MailMessage();
                            string xm = "New Request has been booked with <br>" + mi.Replace("Your Request has been booked with", "");

                            string xmailmsg = xsr.ReadToEnd();
                            xmailmsg = xmailmsg.Replace("{NAME}", mm.Name);
                            xmailmsg = xmailmsg.Replace("{TITLE}", "");
                            xmailmsg = xmailmsg.Replace("{BODY}", xm);
                            xmail.Body = xmailmsg;
                            xsr.Close();
                            Message mms = new Message();
                            mms.Recipient = mm.Email;
                            mms.Title = "Work and Maintenance Department";
                            mms.Mail = xmailmsg;
                            mms.Retries = 0; mms.NotificationStatus = NotificationStatus.NotSent; mms.NotificationType = NotificationType.Email;
                            _context.Messages.Add(mms);
                            await _context.SaveChangesAsync();
                            emx = mms.Id;
                        }
                        //sms
                        if (mm.SendPhone == true)
                        {
                            Message msms = new Message();
                            msms.Recipient = mm.Phone;
                            msms.Title = "Work and Maintenance Department";
                            msms.Mail = "New Request has been booked with Ticket Number (" + Ticket.TicketNumber + "). Kindly Check your email or your dashboard for details";
                            msms.Retries = 0; msms.NotificationStatus = NotificationStatus.NotSent; msms.NotificationType = NotificationType.SMS;
                            _context.Messages.Add(msms);
                            await _context.SaveChangesAsync();
                            ymx = msms.Id;
                        }

                    }
                }
                catch (Exception c)
                {

                }
                TempData["result"] = "Request Made Successful";
                return RedirectToPage("./Index");
            }
            catch (Exception c)
            {
                ViewData["Category"] = new SelectList(_context.TicketCategories, "Title", "Title");

                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                {
                    TempData["usernull"] = "null";
                }
                else
                {
                    Profile = await _context.Profiles.Include(x => x.MyGallery).FirstOrDefaultAsync(x => x.UserId == user.Id);

                }
                return Page();
            }
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
        //public async Task<bool> SendEmail(string recipient, string message, string title)
        //{
        //    try
        //    {


        //        //create the mail message 
        //        MailMessage mail = new MailMessage();


        //        mail.Body = message;
        //        //set the addresses 
        //        mail.From = new MailAddress("noreply@sec44nipss.com", "SEC44 NIPSS"); //IMPORTANT: This must be same as your smtp authentication address.
        //        mail.To.Add(recipient);

        //        //set the content 
        //        mail.Subject = title.Replace("\r\n", "");

        //        mail.IsBodyHtml = true;
        //        //send the message 
        //        SmtpClient smtp = new SmtpClient("mail.sec44nipss.com");

        //        //IMPORANT:  Your smtp login email MUST be same as your FROM address. 
        //        NetworkCredential Credentials = new NetworkCredential("noreply@sec44nipss.com", "Admin@123");
        //        smtp.UseDefaultCredentials = false;
        //        smtp.Credentials = Credentials;
        //        smtp.Port = 25;    //alternative port number is 8889
        //        smtp.EnableSsl = false;
        //        smtp.Send(mail);
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        TempData["err"] = ex.Message.ToString();
        //        return false;
        //    }
        //}

        //public async Task<string> SendSms(string recipient, string message)
        //{


        //    message = message.Replace("0", "O");
        //    message = message.Replace("Services", "Servics");
        //    message = message.Replace("gmail", "g -mail");
        //    string response = "";
        //    //Peter Ahioma

        //    try
        //    {
        //        var getApi = "http://account.kudisms.net/api/?username=peterahioma2020@gmail.com&password=nation@123&message=@@message@@&sender=@@sender@@&mobiles=@@recipient@@";
        //        string apiSending = getApi.Replace("@@sender@@", "SEC 44").Replace("@@recipient@@", HttpUtility.UrlEncode(recipient)).Replace("@@message@@", HttpUtility.UrlEncode(message));

        //        HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(apiSending);
        //        httpWebRequest.Method = "GET";
        //        httpWebRequest.ContentType = "application/json";

        //        //getting the respounce from the request
        //        HttpWebResponse httpWebResponse = (HttpWebResponse)await httpWebRequest.GetResponseAsync();
        //        Stream responseStream = httpWebResponse.GetResponseStream();
        //        StreamReader streamReader = new StreamReader(responseStream);
        //        response = await streamReader.ReadToEndAsync();
        //        //response = "OK";
        //    }
        //    catch (Exception c)
        //    {
        //        response = c.ToString();
        //    }

        //    if (response.ToUpper().Contains("OK") || response.ToUpper().Contains("1701"))
        //    {
        //        return response = "Ok Sent";
        //    }
        //    return response;


        //}


    }

}
