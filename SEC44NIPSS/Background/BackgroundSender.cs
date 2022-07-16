
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SEC44NIPSS.Data;
using SEC44NIPSS.Data.Model;
using SEC44NIPSS.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace SEC44NIPSS.Background
{

    public class RequestInfo
    {
        public string Id { get; set; }
        public string Status { get; set; }
        public string Retries { get; set; }
    }

    public class BackgroundSender : IHostedService, IDisposable
    {
        private readonly ILogger _logger;
        private Timer _timer;



        private readonly IServiceScopeFactory _scopeFactory;
        private readonly INotificationService _notificationService;
        public BackgroundSender(ILogger<BackgroundSender> logger, IServiceScopeFactory scopeFactory, INotificationService notificationService)
        {
            _logger = logger;
            _scopeFactory = scopeFactory;
            _notificationService = notificationService;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Timed Background Service is starting.");

            _timer = new Timer(DoWork, null, TimeSpan.Zero,
                TimeSpan.FromMinutes(8));

            return Task.CompletedTask;
        }
        private async void DoWork(object state)
        {
            _logger.LogInformation("Timed Background Service is working.");

            using (var scope = _scopeFactory.CreateScope())
            {
                var _context = scope.ServiceProvider.GetRequiredService<NIPSSDbContext>();
                IQueryable<Notification> notif = from s in _context.Notifications
                                                 .Include(x => x.UserToNotify)
                                                   .Where(x => x.Sent == false).AsNoTracking()
                                                   //.Where(x => x.DatetTime <= DateTime.UtcNow.AddHours(1).AddMinutes(-5) && x.DatetTime <= DateTime.UtcNow.AddHours(1).AddMinutes(+5))
                                                   .Take(7)
                                                 select s;

                foreach (var nm in notif)
                {
                    NotificationModel notificationModel = new NotificationModel();
                    notificationModel.Body = nm.Message;
                    notificationModel.Title = nm.Title;
                    notificationModel.IsAndroiodDevice = true;
                    notificationModel.DeviceId = nm.UserToNotify.TokenId;
                    await _notificationService.SendNotification(notificationModel);
                    var xiod = await _context.Notifications.AsNoTracking().FirstOrDefaultAsync(x => x.Id == nm.Id);
                    string ixd = xiod.Id.ToString();

                    try
                    {

                        var entry = await _context.Notifications.AsNoTracking().FirstOrDefaultAsync(x => x.Id == Convert.ToInt64(ixd));

                        entry.Sent = true;
                        _context.Attach(entry).State = EntityState.Modified;


                    }
                    catch (Exception e)
                    {
                        var x = "";
                    }

                }
                _context.SaveChanges();
                //Do your stuff with your Dbcontext
                //IQueryable<Message> msgk = from s in _context.Messages
                //                                    .Where(x => x.NotificationStatus == NotificationStatus.NotSent || x.NotificationStatus == NotificationStatus.NotDefind)
                //                                    .Take(7)
                //                           select s;
                //var xc = msgk.Count();
                //var msg = msgk.Where(x => x.Retries < 5);

                //var c = msg.Count();
                //var cf = msg.ToList();

                //foreach (var i in msg)
                //{
                    //if (i.NotificationType == NotificationType.Email)
                    //{
                    //    string xmail = "";
                    //    long xmaiId = 0;
                    //    //
                    //    //bool result = await SendEmail(i.Recipient, i.Mail, i.Title);
                    //    bool result = false;
                    //    try
                    //    {
                            
                    //        var email = await _context.EmailSettings.AsNoTracking().FirstOrDefaultAsync(x => x.Count < 45 && x.DateStart < DateTime.Now.AddHours(2));
                    //        if (email != null)
                    //        {
                    //            xmaiId = email.Id;
                    //            try
                    //            {
                    //                //create the mail message 
                    //                MailMessage mail = new MailMessage();


                    //                mail.Body = i.Mail;
                    //                //set the addresses 
                    //                mail.From = new MailAddress(email.SenderEmail, "SEC44 NIPSS"); //IMPORTANT: This must be same as your smtp authentication address.
                    //                mail.To.Add(i.Recipient);

                    //                //set the content 
                    //                mail.Subject = i.Title;

                    //                mail.IsBodyHtml = true;
                    //                //send the message 
                    //                SmtpClient smtp = new SmtpClient("mail.sec44nipss.com");

                    //                //IMPORANT:  Your smtp login email MUST be same as your FROM address. 
                    //                NetworkCredential Credentials = new NetworkCredential(email.SenderEmail, email.PX);
                    //                smtp.UseDefaultCredentials = false;
                    //                smtp.Credentials = Credentials;
                    //                smtp.Port = 25;    //alternative port number is 8889
                    //                smtp.EnableSsl = false;
                    //                smtp.Send(mail);
                    //                xmail = email.SenderEmail;
                    //                result = true;
                    //            }
                    //            catch (Exception d)
                    //            {
                    //                MailMessage mail = new MailMessage();


                    //                mail.Body = d.ToString();
                    //                //set the addresses 
                    //                mail.From = new MailAddress("noreply@ahioma.ng", "NIPSS"); //IMPORTANT: This must be same as your smtp authentication address.
                    //                mail.To.Add("onwukaemeka41@gmail.com");

                    //                //set the content 
                    //                mail.Subject = "Error Check";

                    //                mail.IsBodyHtml = true;
                    //                //send the message 
                    //                SmtpClient smtp = new SmtpClient("mail.ahioma.ng");

                    //                //IMPORANT:  Your smtp login email MUST be same as your FROM address. 
                    //                NetworkCredential Credentials = new NetworkCredential("noreply@ahioma.ng", "Admin@123");
                    //                smtp.UseDefaultCredentials = false;
                    //                smtp.Credentials = Credentials;
                    //                smtp.Port = 25;    //alternative port number is 8889
                    //                smtp.EnableSsl = false;
                    //                smtp.Send(mail);
                    //                result = false;
                    //            }
                    //        }
                    //        else
                    //        {
                    //            var s = await SendSms("08165680904", "no available e-m-ail to send item");
                    //            result = false;
                    //        }

                    //    }
                    //    catch (Exception ex)
                    //    {

                    //        result = false;
                    //    }

                    //    var updatemail = await _context.EmailSettings.AsNoTracking().FirstOrDefaultAsync(x => x.Id == xmaiId);
                    //    if (result == true)
                    //    {
                    //        i.NotificationStatus = NotificationStatus.Sent;
                    //        updatemail.Count = updatemail.Count + 1;
                    //    }
                    //    else
                    //    {
                    //        i.NotificationStatus = NotificationStatus.NotSent;
                    //        i.Retries = i.Retries + 1;
                    //    }
                    //    _context.Attach(updatemail).State = EntityState.Modified;
                    //    i.SentVia = xmail;
                    //}
                    //else if (i.NotificationType == NotificationType.SMS)
                    //{
                    //    //
                    //    string result = await SendSms(i.Recipient, i.Mail);
                        
                    //    if (result.Contains("OK"))
                    //    {
                    //        i.NotificationStatus = NotificationStatus.Sent;
                    //    }
                    //    else
                    //    {
                    //        i.NotificationStatus = NotificationStatus.NotSent;
                    //        i.Retries = i.Retries + 1;
                    //    }
                        
                    //}

                    //try
                    //{

                    //    var iod = await _context.Messages.AsNoTracking().FirstOrDefaultAsync(x => x.Id == i.Id);
                    //    iod.NotificationStatus = i.NotificationStatus;
                    //    iod.Retries = i.Retries;
                    //    _context.Attach(iod).State = EntityState.Modified;
                        


                    //}

                    //catch (Exception webex)
                    //{

                    //}


               // }
                await _context.SaveChangesAsync();
            }
        }
        //private async void DoWork(object state)
        //{
        //    _logger.LogInformation("Timed Background Service is working.");
        //    await _senderService.Notification();
        //}

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Timed Background Service is stopping.");

            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public async Task<string> SendSms(string recipient, string message)
        {


            message = message.Replace("0", "O");
            message = message.Replace("Services", "Servics");
            message = message.Replace("gmail", "g -mail");
            string response = "";
            //Peter Ahioma

            try
            {
                var getApi = "http://account.kudisms.net/api/?username=peterahioma2020@gmail.com&password=nation@123&message=@@message@@&sender=@@sender@@&mobiles=@@recipient@@";
                string apiSending = getApi.Replace("@@sender@@", "SEC 44").Replace("@@recipient@@", HttpUtility.UrlEncode(recipient)).Replace("@@message@@", HttpUtility.UrlEncode(message));

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
                return response = "Ok Sent";
            }
            return response;


        }


       



        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
