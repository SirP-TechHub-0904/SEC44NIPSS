
using Google;
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
                TimeSpan.FromMinutes(1));

            return Task.CompletedTask;
        }
        private async void DoWork(object state)
        {
            _logger.LogInformation("Timed Background Service is working.");

            using (var scope = _scopeFactory.CreateScope())
            {
                var _context = scope.ServiceProvider.GetRequiredService<NIPSSDbContext>();
                ////Do your stuff with your Dbcontext
                IQueryable<Message> msgk = from s in _context.Messages
                                                    .Where(x => x.NotificationStatus == NotificationStatus.NotSent && x.Retries < 4)

                                                    .Take(15)
                                           select s;

                var msg = msgk;

                var c = msg.Count();
                var cf = msg.ToList();

                foreach (var i in msg)
                {
                    if (i.NotificationType == NotificationType.Email)
                    {

                        string result = await SendEmail(i.Recipient, i.Mail, i.Title);
                        if (result == "true")
                        {
                            i.NotificationStatus = NotificationStatus.Sent;
                            i.Result = "sent";
                            i.DateSent = DateTime.UtcNow.AddHours(1);
                        }
                        else
                        {
                            i.NotificationStatus = NotificationStatus.NotSent;
                            i.Retries = i.Retries + 1;
                            i.Result = result; i.DateSent = DateTime.UtcNow.AddHours(1);
                        }
                    }
                    else if (i.NotificationType == NotificationType.SMS)
                    {

                        string result = await SendSms(i.Recipient, i.Mail);
                        if (result.Contains("OK"))
                        {
                            i.NotificationStatus = NotificationStatus.Sent;
                            i.Result = "sent"; i.DateSent = DateTime.UtcNow.AddHours(1);
                        }
                        else
                        {
                            i.NotificationStatus = NotificationStatus.NotSent;
                            i.Retries = i.Retries + 1;
                            i.Result = result; i.DateSent = DateTime.UtcNow.AddHours(1);
                        }
                    }

                    try
                    {

                        var iod = await _context.Messages.AsNoTracking().FirstOrDefaultAsync(x => x.Id == i.Id);
                        iod.NotificationStatus = i.NotificationStatus;
                        iod.Retries = i.Retries;
                        iod.DateSent = DateTime.UtcNow.AddHours(1);
                        //_context.Entry(iod).State = EntityState.Detached;
                        _context.Attach(iod).State = EntityState.Modified;


                    }

                    catch (Exception webex)
                    {

                    }


                }
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (Exception h) { }












                //var _context = scope.ServiceProvider.GetRequiredService<NIPSSDbContext>();
                //////comment page
                //#region Seelction comment page

                //try
                //{
                //    var smsmessageitem = await _context.Messages.AsNoTracking().OrderBy(x => x.Id).Where(x => x.NotificationStatus == NotificationStatus.NotSent & x.Retries < 5 && x.NotificationType == NotificationType.SMS).Take(10).ToListAsync();
                //    foreach (var xm in smsmessageitem)
                //    {
                //        if (xm.NotificationType == NotificationType.SMS)
                //        {
                //            //
                //            string result = await SendSms(xm.Recipient, xm.Mail);

                //            if (result.Contains("OK"))
                //            {
                //                xm.NotificationStatus = NotificationStatus.Sent;
                //                xm.Result = result;

                //                xm.DateSent = DateTime.UtcNow.AddHours(1);
                //            }
                //            else
                //            {
                //                xm.NotificationStatus = NotificationStatus.NotSent;
                //                xm.Retries = xm.Retries + 1;
                //                xm.Result = result;

                //                xm.DateSent = DateTime.UtcNow.AddHours(1);
                //            }
                //            _context.Attach(xm).State = EntityState.Modified;


                //        }
                //    }
                //    await _context.SaveChangesAsync();
                //}
                //catch (Exception s) { }

                //#region comment page

                //IQueryable<Notification> notif = from s in _context.Notifications
                //                                 .Include(x => x.UserToNotify)
                //                                   .Where(x => x.Sent == false).AsNoTracking()
                //                                   //.Where(x => x.DatetTime <= DateTime.UtcNow.AddHours(1).AddMinutes(-5) && x.DatetTime <= DateTime.UtcNow.AddHours(1).AddMinutes(+5))
                //                                   .Take(2)
                //                                 select s;

                //foreach (var nm in notif)
                //{
                //    NotificationModel notificationModel = new NotificationModel();
                //    notificationModel.Body = nm.Message;
                //    notificationModel.Title = nm.Title;
                //    notificationModel.IsAndroiodDevice = true;
                //    notificationModel.DeviceId = nm.UserToNotify.TokenId;
                //    await _notificationService.SendNotification(notificationModel);
                //    var xiod = await _context.Notifications.AsNoTracking().FirstOrDefaultAsync(x => x.Id == nm.Id);
                //    string ixd = xiod.Id.ToString();

                //    try
                //    {

                //        var entry = await _context.Notifications.AsNoTracking().FirstOrDefaultAsync(x => x.Id == Convert.ToInt64(ixd));

                //        entry.Sent = true;
                //        _context.Attach(entry).State = EntityState.Modified;


                //    }
                //    catch (Exception e)
                //    {
                //        var x = "";
                //    }

                //}
                //_context.SaveChanges();
                ////Do your stuff with your Dbcontext
                //#region main
                //#region send and receive
                //try
                //{
                //    var messageitem = await _context.Messages.OrderBy(x => x.Id).FirstOrDefaultAsync(x => x.NotificationStatus == NotificationStatus.NotSent & x.Retries < 5 && x.NotificationType == NotificationType.Email);
                //    var xemail = await _context.EmailSettings.AsNoTracking().FirstOrDefaultAsync(x => x.Active == true);

                //    if (xemail.Count >= 48)
                //    {
                //        //
                //        var resetactiveemail = await _context.EmailSettings.AsNoTracking().FirstOrDefaultAsync(x => x.Id == xemail.Id);
                //        resetactiveemail.Count = 0;
                //        resetactiveemail.Active = false;
                //        resetactiveemail.DateStart = resetactiveemail.DateStart.AddHours(1);
                //        _context.Attach(resetactiveemail).State = EntityState.Modified;
                //        await _context.SaveChangesAsync();

                //        //
                //        var xchoseemail = await _context.EmailSettings.AsNoTracking().Where(x => x.Active == false).ToListAsync();
                //        var choseemail = xchoseemail.Where(x => x.DateStart.AddHours(-1) < xemail.DateStart).FirstOrDefault();
                //        if (choseemail == null)
                //        {
                //            return;
                //        }
                //        choseemail.Active = true;
                //        _context.Attach(choseemail).State = EntityState.Modified;
                //        await _context.SaveChangesAsync();


                //        // var checkemail1 = await _context.EmailSettings.AsNoTracking().FirstOrDefaultAsync(x => x.Count >= 48 && x.DateStart < DateTime.Now.AddHours(1) & x.Active == true);

                //    }
                //    if (messageitem != null)
                //    {
                //        if (messageitem.NotificationType == NotificationType.Email)
                //        {

                //            try
                //            {
                //                //var checkemail = await _context.EmailSettings.AsNoTracking().FirstOrDefaultAsync(x => x.Count >= 48 && x.DateStart < DateTime.Now.AddHours(1) & x.Active == true);
                //                var email = await _context.EmailSettings.AsNoTracking().FirstOrDefaultAsync(x => x.Active == true);
                //                if (email != null)
                //                {
                //                    string sendresult = "";

                //                    try
                //                    {


                //                        //create the mail message 
                //                        MailMessage mail = new MailMessage();


                //                        mail.Body = messageitem.Mail;
                //                        //set the addresses 
                //                        mail.From = new MailAddress(email.SenderEmail, "SEC44 NIPSS"); //IMPORTANT: This must be same as your smtp authentication address.
                //                        mail.To.Add(messageitem.Recipient);

                //                        //set the content 
                //                        mail.Subject = messageitem.Title.Replace("\r\n", "");

                //                        mail.IsBodyHtml = true;
                //                        //send the message 
                //                        SmtpClient smtp = new SmtpClient("mail.sec44nipss.com");

                //                        //IMPORANT:  Your smtp login email MUST be same as your FROM address. 
                //                        NetworkCredential Credentials = new NetworkCredential(email.SenderEmail, email.PX);
                //                        smtp.UseDefaultCredentials = false;
                //                        smtp.Credentials = Credentials;
                //                        smtp.Port = 25;    //alternative port number is 8889
                //                        smtp.EnableSsl = false;
                //                        smtp.Send(mail);
                //                        sendresult = "true";
                //                    }
                //                    catch (Exception exc)
                //                    {
                //                        try
                //                        {

                //                            MailMessage mail = new MailMessage();
                //                            //set the addresses 
                //                            mail.From = new MailAddress("espErrorMail@exwhyzee.ng"); //IMPORTANT: This must be same as your smtp authentication address.
                //                            mail.To.Add("espErrorMail@exwhyzee.ng");
                //                            mail.To.Add("iskoolsportal@gmail.com");
                //                            mail.Subject = "Error sec44nipss ";
                //                            mail.Body = exc.ToString();
                //                            //send the message 
                //                            SmtpClient smtp = new SmtpClient("mail.exwhyzee.ng");

                //                            //IMPORANT:  Your smtp login email MUST be same as your FROM address. 
                //                            NetworkCredential Credentials = new NetworkCredential("espErrorMail@exwhyzee.ng", "Exwhyzee@123");
                //                            smtp.Credentials = Credentials;
                //                            smtp.Send(mail);

                //                        }
                //                        catch (Exception ex)
                //                        {


                //                        }
                //                        if (exc.ToString().Contains("memory"))
                //                        {
                //                            sendresult = "memory";
                //                        }
                //                        sendresult = exc.ToString();
                //                    }



                //                    if (sendresult == "true")
                //                    {
                //                        messageitem.NotificationStatus = NotificationStatus.Sent;
                //                        messageitem.Result = sendresult;
                //                        messageitem.SentVia = email.SenderEmail;
                //                        messageitem.DateSent = DateTime.UtcNow.AddHours(1);
                //                        _context.Attach(messageitem).State = EntityState.Modified;
                //                        //
                //                        email.Count = email.Count + 1;
                //                        email.DateStart = DateTime.UtcNow.AddHours(1);
                //                        _context.Attach(email).State = EntityState.Modified;



                //                    }
                //                    else if (sendresult == "memory")
                //                    {

                //                    }
                //                    else
                //                    {
                //                        messageitem.NotificationStatus = NotificationStatus.NotSent;
                //                        messageitem.Retries = messageitem.Retries + 1;
                //                        messageitem.Result = sendresult;
                //                        messageitem.SentVia = email.SenderEmail;

                //                        messageitem.DateSent = DateTime.UtcNow.AddHours(1);
                //                        _context.Attach(messageitem).State = EntityState.Modified;

                //                    }
                //                }
                //                await _context.SaveChangesAsync();

                //            }
                //            catch (Exception ex)
                //            {
                //            }
                //        }
                //    }
                //}
                //catch (Exception c) { }

                //////

                //#endregion
                //#endregion

                //#endregion
                //#endregion
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
                var getApi = "http://account.kudisms.net/api/?username=ponwuka123@gmail.com&password=sms@123&message=@@message@@&sender=@@sender@@&mobiles=@@recipient@@";
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
                mail.From = new MailAddress("admin@sec44nipss.com", "SEC44 NIPSS"); //IMPORTANT: This must be same as your smtp authentication address.
                mail.To.Add(recipient);

                //set the content 
                mail.Subject = title.Replace("\r\n", "");

                mail.IsBodyHtml = true;
                //send the message 
                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);

                //IMPORANT:  Your smtp login email MUST be same as your FROM address. 
                NetworkCredential Credentials = new NetworkCredential("admin@sec44nipss.com", "yvjholmmateuysye");
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = Credentials;
                smtp.Timeout = 20000;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.EnableSsl = true;
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
