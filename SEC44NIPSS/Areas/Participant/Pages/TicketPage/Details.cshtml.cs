using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Mail;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SEC44NIPSS.Data;
using SEC44NIPSS.Data.Model;

namespace SEC44NIPSS.Areas.Participant.Pages.TicketPage
{
    public class DetailsModel : PageModel
    {
        private readonly SEC44NIPSS.Data.NIPSSDbContext _context;
        private readonly IHostingEnvironment _hostingEnv;
        private readonly UserManager<IdentityUser> _userManager;
        public DetailsModel(SEC44NIPSS.Data.NIPSSDbContext context, IHostingEnvironment hostingEnv, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _hostingEnv = hostingEnv;
            _userManager = userManager;
        }
        [BindProperty]
        public Ticket Ticket { get; set; }
        [BindProperty]
        public long TicketId { get; set; }
        [BindProperty]
        public Profile Profile { get; set; }

        [BindProperty]
        public string ChatMessage { get; set; }


        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Ticket = await _context.Tickets
                .Include(t => t.ApprovedBy)
                .Include(t => t.ForwardedTo)
                .Include(t => t.JobCompletionCertifiedBy)
                .Include(t => t.ReceivedAndPassTo).Include(t => t.User)
                .Include(t => t.TicketResponses)
                .ThenInclude(t => t.Profile)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (Ticket == null)
            {
                return NotFound();
            }
            return Page();
        }
        
        public async Task<IActionResult> OnPostSubmit()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                Profile = await _context.Profiles.Include(x => x.MyGallery).FirstOrDefaultAsync(x => x.UserId == user.Id);
            }
           
            TicketResponse n = new TicketResponse();
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

                        n.Image = $"{fileDbPathName}{newFileNamex}";


                        fullPath = fileName;



                        if (imgCount >= 5)
                            break;
                    }
                }
            }

            n.ProfileId = Profile.Id;
            n.TicketId = TicketId;
            n.Reply = ChatMessage;
            n.CreatedTime = DateTime.UtcNow.AddHours(1);
            _context.TicketResponses.Add(n);
            await _context.SaveChangesAsync();
            var tik = await _context.Tickets.FirstOrDefaultAsync(x => x.Id == TicketId);
           
            StreamReader sr = new StreamReader(System.IO.Path.Combine(_hostingEnv.WebRootPath, "emailsec.html"));
            MailMessage mail = new MailMessage();
            string mi = $"Ticket Reply # (" + tik.TicketNumber + ") <br><br>" +
                $"================================<br>" +
                $"" + ChatMessage +
                $"<br>================================<br>" +
                $"<img src=\"https://sec44nipss.com"+n.Image+"\" style=\"height:200px\"><br>";

            string mailmsg = sr.ReadToEnd();
            mailmsg = mailmsg.Replace("{NAME}", Profile.FullName);
            mailmsg = mailmsg.Replace("{TITLE}", "");
            mailmsg = mailmsg.Replace("{BODY}", mi);
            mail.Body = mailmsg;
            sr.Close();

            Message ms = new Message();
            ms.Recipient = user.Email;
            ms.Title = "Ticket {"+tik.TicketNumber+"} has been Updated";
            ms.Mail = mailmsg;
            ms.Retries = 0; ms.NotificationStatus = NotificationStatus.NotSent; ms.NotificationType = NotificationType.Email;
            _context.Messages.Add(ms);
             await _context.SaveChangesAsync();

            //sms
            Message sms = new Message();
            sms.Recipient = Profile.PhoneNumber;
            sms.Title = "Ticket {" + tik.TicketNumber + "} has been Updated";
            sms.Mail = "Ticket {" + tik.TicketNumber + "} has been Updated";
            sms.Retries = 0; sms.NotificationStatus = NotificationStatus.NotSent; sms.NotificationType = NotificationType.SMS;
            _context.Messages.Add(sms);
            await _context.SaveChangesAsync();

            var callbackUrl = Url.Page(
                      "/MaintainancePage/Details",
                      pageHandler: null,
                      values: new { area = "NIPSS", id = TicketId },
                      protocol: Request.Scheme);

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
                        string xm = "Ticket Response <br>================================<br><br>" + Profile.FullName + " replied his ticket # (" + tik.TicketNumber + ") <br>" +
                            $" <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here to comfirm</a>.<br><br>================================<br>" + mi;

                        string xmailmsg = xsr.ReadToEnd();
                        xmailmsg = xmailmsg.Replace("{NAME}", mm.Name);
                        xmailmsg = xmailmsg.Replace("{TITLE}", "");
                        xmailmsg = xmailmsg.Replace("{BODY}", xm);
                        xmail.Body = xmailmsg;
                        xsr.Close();

                        Message mms = new Message();
                        mms.Recipient = mm.Email;
                        mms.Title = "Ticket {" + tik.TicketNumber + "} has been Updated";
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
                        msms.Title = "Ticket {" + tik.TicketNumber + "} has been Updated";
                        msms.Mail = "Ticket {" + tik.TicketNumber + "} has been Updated";
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

            return RedirectToPage("./Details", new { id = TicketId });
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
