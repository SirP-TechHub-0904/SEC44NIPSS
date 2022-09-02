using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SEC44NIPSS.Data;
using SEC44NIPSS.Data.Model;

namespace SEC44NIPSS.Areas.Admin.Pages.Rapid
{
    public class GenerateRapidModel : PageModel
    {
        private readonly SEC44NIPSS.Data.NIPSSDbContext _context;
        private readonly IHostingEnvironment _hostingEnv;
        private readonly UserManager<IdentityUser> _userManager;


        public GenerateRapidModel(SEC44NIPSS.Data.NIPSSDbContext context, IHostingEnvironment hostingEnv, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _hostingEnv = hostingEnv;
            _userManager = userManager;
        }

        public IActionResult OnGet()
        {
        ViewData["ProfileId"] = new SelectList(_context.Profiles, "Id", "Id");
            return Page();
        }

        [BindProperty]
        public UserAnswer UserAnswer { get; set; }

      
        [BindProperty]
        public int Skip { get; set; }

        [BindProperty]
        public int Take { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
           var Profile = await _context.Profiles
                                         .Include(p => p.Alumni)
                                         .Include(p => p.StudyGroupMemeber)
                                         .ThenInclude(x => x.StudyGroup)
                                         .Include(p => p.User)
                                         .OrderBy(x => x.FullName).ToListAsync();
            var xcheck = Profile.Where(x => x.User != null).Skip(Skip).Take(Take);
            var xcx = xcheck;
            var inos = DateTime.UtcNow.ToString("MMddHmmssyyyy");
            foreach (var x in xcx.ToList())
            {
                UserAnswer ass = new UserAnswer();

                ass.Date = DateTime.UtcNow.AddHours(1);
                ass.CombineCode = inos;
                ass.ProfileId = x.Id;
                ass.Title = UserAnswer.Title;                
                _context.UserAnswers.Add(ass);

                await _context.SaveChangesAsync();

                //https://sec44nipss.com/Participant/Dashboard/StartPreview?id=24
                var callbackUrl = Url.Page(
                      "/Dashboard/StartPreview",
                      pageHandler: null,
                      values: new { area = "Participant", id = ass.Id },
                      protocol: Request.Scheme);
                //
                StreamReader sr = new StreamReader(System.IO.Path.Combine(_hostingEnv.WebRootPath, "emailsec.html"));
                MailMessage mail = new MailMessage();
                string mi = $"<h4>RAPID TEST</h4><br>A new test has been created for you to test your efficiency<br><br>" +
                    $" <a href='{HtmlEncoder.Default.Encode(callbackUrl)}' class=\"btn btn-primary\">clicking here to start</a>.<br><br>================================<br>" +
                    $"Its just 5min test.<br>";
                    

                string mailmsg = sr.ReadToEnd();
                mailmsg = mailmsg.Replace("{NAME}", x.FullName);
                mailmsg = mailmsg.Replace("{TITLE}", "");
                mailmsg = mailmsg.Replace("{BODY}", mi);
                mail.Body = mailmsg;
                sr.Close();

                //sms
                Message sms = new Message();
                sms.Recipient = x.PhoneNumber;
                sms.Title = "Rapid Test Game";
                sms.Mail = "A new test has been created for you to test your efficiency\n" + callbackUrl + "\n Kindly to continue. Its just 5min";
                sms.Retries = 0;  sms.NotificationType = NotificationType.SMS;
                

                try
                {

                    sms.Mail = sms.Mail.Replace("0", "O");
                    sms.Mail = sms.Mail.Replace("Services", "Servics");
                    sms.Mail = sms.Mail.Replace("gmail", "g -mail");
                    string response = "";
                    //Peter Ahioma

                    try
                    {
                        var getApi = "http://account.kudisms.net/api/?username=ponwuka123@gmail.com&password=sms@123&message=@@message@@&sender=@@sender@@&mobiles=@@recipient@@";
                        string apiSending = getApi.Replace("@@sender@@", "SEC 44").Replace("@@recipient@@", HttpUtility.UrlEncode(sms.Recipient)).Replace("@@message@@", HttpUtility.UrlEncode(sms.Mail));

                        HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(apiSending);
                        httpWebRequest.Method = "GET";
                        httpWebRequest.ContentType = "application/json";

                        //getting the respounce from the request
                        HttpWebResponse httpWebResponse = (HttpWebResponse)await httpWebRequest.GetResponseAsync();
                        Stream responseStream = httpWebResponse.GetResponseStream();
                        StreamReader streamReader = new StreamReader(responseStream);
                        response = await streamReader.ReadToEndAsync();
                        //response = "OK";
                    }
                    catch (Exception c)
                    {
                        response = c.ToString();
                    }

                    if (response.ToUpper().Contains("OK") || response.ToUpper().Contains("1701"))
                    {
                        sms.NotificationStatus = NotificationStatus.Sent;
                        sms.DateSent = DateTime.UtcNow.AddHours(1);
                    }
                    else
                    {
                        sms.NotificationStatus = NotificationStatus.NotSent;
                        sms.Retries = sms.Retries + 1;
                        sms.DateSent = DateTime.UtcNow.AddHours(1);
                    }

                }catch(Exception c) { }
                
                _context.Messages.Add(sms);

                await _context.SaveChangesAsync();
            }

            TempData["success"] = "success";
            

            return RedirectToPage("./GenerateRapid");
        }
    }
}
