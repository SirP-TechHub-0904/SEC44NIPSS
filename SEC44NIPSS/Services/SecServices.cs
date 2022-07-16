using Microsoft.EntityFrameworkCore;
using SEC44NIPSS.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;

namespace SEC44NIPSS.Services
{
    public class SecServices : ISecServices
    {

        private readonly NIPSSDbContext _context;
        public SecServices(NIPSSDbContext context)
        {
            _context = context;
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
                string apiSending = getApi.Replace("@@sender@@", "Ahioma").Replace("@@recipient@@", HttpUtility.UrlEncode(recipient)).Replace("@@message@@", HttpUtility.UrlEncode(message));

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


        public async Task<bool> SendEmail(string recipient, string message, string title)
        {
            try
            {

                var email = await _context.EmailSettings.FirstOrDefaultAsync(x => x.Count < 45 && x.DateStart > DateTime.Now.AddHours(2));
                if (email != null)
                {
                    try
                    {
                        //create the mail message 
                        MailMessage mail = new MailMessage();


                        mail.Body = message;
                        //set the addresses 
                        mail.From = new MailAddress(email.SenderEmail, "SEC44 NIPSS"); //IMPORTANT: This must be same as your smtp authentication address.
                        mail.To.Add(recipient);

                        //set the content 
                        mail.Subject = title;

                        mail.IsBodyHtml = true;
                        //send the message 
                        SmtpClient smtp = new SmtpClient("mail.sec44nipss.com");

                        //IMPORANT:  Your smtp login email MUST be same as your FROM address. 
                        NetworkCredential Credentials = new NetworkCredential(email.SenderEmail, email.PX);
                        smtp.UseDefaultCredentials = false;
                        smtp.Credentials = Credentials;
                        smtp.Port = 25;    //alternative port number is 8889
                        smtp.EnableSsl = false;
                        smtp.Send(mail);
                        return true;
                    }
                    catch (Exception d)
                    {
                        MailMessage mail = new MailMessage();


                        mail.Body = d.ToString();
                        //set the addresses 
                        mail.From = new MailAddress("noreply@ahioma.ng", "NIPSS"); //IMPORTANT: This must be same as your smtp authentication address.
                        mail.To.Add("onwukaemeka41@gmail.com");

                        //set the content 
                        mail.Subject = title;

                        mail.IsBodyHtml = true;
                        //send the message 
                        SmtpClient smtp = new SmtpClient("mail.ahioma.ng");

                        //IMPORANT:  Your smtp login email MUST be same as your FROM address. 
                        NetworkCredential Credentials = new NetworkCredential("noreply@ahioma.ng", "Admin@123");
                        smtp.UseDefaultCredentials = false;
                        smtp.Credentials = Credentials;
                        smtp.Port = 25;    //alternative port number is 8889
                        smtp.EnableSsl = false;
                        smtp.Send(mail);
                        return false;
                    }
                }
                else
                {
                    var s = await SendSms("08165680904", "no available e-m-ail to send item");
                    return false;
                }

            }
            catch (Exception ex)
            {

                return false;
            }
        }

    }
}
