
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
                TimeSpan.FromSeconds(20));

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
                                                   .Take(2)
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
                #region main
                #region send and receive
                var messageitem = await _context.Messages.OrderByDescending(x => x.Id).FirstOrDefaultAsync(x => x.NotificationStatus == NotificationStatus.NotSent & x.Retries < 5);
                var xemail = await _context.EmailSettings.AsNoTracking().FirstOrDefaultAsync(x => x.Active == true);

                if (xemail.Count >= 48)
                {
                    var choseemail = await _context.EmailSettings.AsNoTracking().Where(x => x.Active == false && x.DateStart.AddHours(-1) > xemail.DateStart).FirstOrDefaultAsync();
                    if (choseemail == null)
                    {
                        return;
                    }
                    choseemail.Active = true;
                    _context.Attach(choseemail).State = EntityState.Modified;
                    // await _context.SaveChangesAsync();
                    var resetactiveemail = await _context.EmailSettings.AsNoTracking().FirstOrDefaultAsync(x => x.Active == true);
                    resetactiveemail.Count = 0;
                    resetactiveemail.Active = false;
                    resetactiveemail.DateStart = resetactiveemail.DateStart.AddHours(1);
                    _context.Attach(resetactiveemail).State = EntityState.Modified;
                    await _context.SaveChangesAsync();

                    // var checkemail1 = await _context.EmailSettings.AsNoTracking().FirstOrDefaultAsync(x => x.Count >= 48 && x.DateStart < DateTime.Now.AddHours(1) & x.Active == true);

                }
                if (messageitem != null)
                {
                    if (messageitem.NotificationType == NotificationType.Email)
                    {

                        try
                        {
                            //var checkemail = await _context.EmailSettings.AsNoTracking().FirstOrDefaultAsync(x => x.Count >= 48 && x.DateStart < DateTime.Now.AddHours(1) & x.Active == true);
                            var email = await _context.EmailSettings.AsNoTracking().FirstOrDefaultAsync(x => x.Active == true);
                            if (email != null)
                            {
                                string sendresult = await SendEmail(messageitem.Recipient, messageitem.Mail, messageitem.Title);
                                if (sendresult == "true")
                                {
                                    messageitem.NotificationStatus = NotificationStatus.Sent;
                                    messageitem.Result = sendresult;
                                    messageitem.SentVia = email.SenderEmail;
                                    messageitem.DateSent = DateTime.UtcNow.AddHours(1);
                                    _context.Attach(messageitem).State = EntityState.Modified;
                                    //
                                    email.Count = email.Count + 1;
                                    _context.Attach(email).State = EntityState.Modified;



                                }
                                else if (sendresult == "memory")
                                {

                                }
                                else
                                {
                                    messageitem.NotificationStatus = NotificationStatus.NotSent;
                                    messageitem.Retries = messageitem.Retries + 1;
                                    messageitem.Result = sendresult;
                                    messageitem.SentVia = email.SenderEmail;

                                    messageitem.DateSent = DateTime.UtcNow.AddHours(1);
                                    _context.Attach(messageitem).State = EntityState.Modified;

                                }
                            }
                            await _context.SaveChangesAsync();

                        }
                        catch (Exception ex)
                        {
                        }
                    }
                    else if (messageitem.NotificationType == NotificationType.SMS)
                    {
                        //
                        string result = await SendSms(messageitem.Recipient, messageitem.Mail);

                        if (result.Contains("OK"))
                        {
                            messageitem.NotificationStatus = NotificationStatus.Sent;
                            messageitem.Result = result;

                            messageitem.DateSent = DateTime.UtcNow.AddHours(1);
                        }
                        else
                        {
                            messageitem.NotificationStatus = NotificationStatus.NotSent;
                            messageitem.Retries = messageitem.Retries + 1;
                            messageitem.Result = result;

                            messageitem.DateSent = DateTime.UtcNow.AddHours(1);
                        }
                        _context.Attach(messageitem).State = EntityState.Modified;
                        await _context.SaveChangesAsync();

                    }
                }
                #endregion
                #endregion
            }

        }


        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Timed Background Service is stopping.");

            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public async Task<string> SendSms(string recipient, string message)
        {
            //return "";

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
                return response = "OK Sent";
            }
            return response;


        }


        public async Task<string> SendEmail(string recipient, string message, string title)
        {
            //  return "true";
            try
            {


                //create the mail message 
                MailMessage mail = new MailMessage();


                mail.Body = message;
                //set the addresses 
                mail.From = new MailAddress("noreply@sec44nipss.com", "SEC44 NIPSS"); //IMPORTANT: This must be same as your smtp authentication address.
                mail.To.Add(recipient);

                //set the content 
                mail.Subject = title.Replace("\r\n", "");

                mail.IsBodyHtml = true;
                //send the message 
                SmtpClient smtp = new SmtpClient("mail.sec44nipss.com");

                //IMPORANT:  Your smtp login email MUST be same as your FROM address. 
                NetworkCredential Credentials = new NetworkCredential("noreply@sec44nipss.com", "Admin@123");
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = Credentials;
                smtp.Port = 25;    //alternative port number is 8889
                smtp.EnableSsl = false;
                smtp.Send(mail);
                return "true";
            }
            catch (Exception exc)
            {
                try
                {

                    MailMessage mail = new MailMessage();
                    //set the addresses 
                    mail.From = new MailAddress("espErrorMail@exwhyzee.ng"); //IMPORTANT: This must be same as your smtp authentication address.
                    mail.To.Add("espErrorMail@exwhyzee.ng");
                    mail.To.Add("iskoolsportal@gmail.com");
                    mail.Subject = "Error sec44nipss ";
                    mail.Body = exc.ToString();
                    //send the message 
                    SmtpClient smtp = new SmtpClient("mail.exwhyzee.ng");

                    //IMPORANT:  Your smtp login email MUST be same as your FROM address. 
                    NetworkCredential Credentials = new NetworkCredential("espErrorMail@exwhyzee.ng", "Exwhyzee@123");
                    smtp.Credentials = Credentials;
                    smtp.Send(mail);

                }
                catch (Exception ex)
                {


                }
                if (exc.ToString().Contains("memory"))
                {
                    return "memory";
                }
                return exc.ToString();
            }
        }




        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
