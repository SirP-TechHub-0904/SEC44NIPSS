using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SEC44NIPSS.Data.Model;

namespace SEC44NIPSS.Areas.Admin.Pages.MessagingPage
{
    public class ValidateModel : PageModel
    {
        private readonly SEC44NIPSS.Data.NIPSSDbContext _context;

        public ValidateModel(SEC44NIPSS.Data.NIPSSDbContext context)
        {
            _context = context;
        }



        public async Task OnGetAsync()
        {

            IQueryable<Message> msgk = from s in _context.Messages.AsNoTracking()
                                                    .Where(x => x.NotificationStatus == NotificationStatus.NotSent || x.NotificationStatus == NotificationStatus.NotDefind).OrderByDescending(x => x.Date)
                                                    .Where(x => x.Retries < 5)
                                                    .Take(7)
                                       select s;
            var xc = msgk.Count();
            var msg = msgk.ToList();

            var c = msg.Count();
            var cf = msg.ToList();
            int emailcounting = 0;
            foreach (var i in msg)
            {
                if (i.NotificationType == NotificationType.Email)
                {
                    string xmail = "";
                    long xmaiId = 0;
                    //
                    //bool result = await SendEmail(i.Recipient, i.Mail, i.Title);
                    bool result = false;
                    try
                    {

                        var email = await _context.EmailSettings.AsNoTracking().FirstOrDefaultAsync(x => x.Count < 45 && x.Active == true);
                        if (email != null)
                        {
                            xmaiId = email.Id;
                            try
                            {

                                result = true;
                                email.Count = email.Count + 1;
                                if(email.Count >= 48)
                                {
                                    email.Active = false;
                                }
                                _context.Attach(email).State = EntityState.Modified;
                                emailcounting = email.Count;

                            }
                            catch (Exception d)
                            {

                                result = false;
                            }
                        }
                        else
                        {
                            //var s = await SendSms("08165680904", "no available e-m-ail to send item");
                            result = false;
                        }

                    }
                    catch (Exception ex)
                    {

                        result = false;
                    }

                    if (result == true)
                    {
                        i.NotificationStatus = NotificationStatus.Sent;
                       
                    }
                    else
                    {
                        i.NotificationStatus = NotificationStatus.NotSent;
                        i.Retries = i.Retries + 1;
                    }
                    

                    if (emailcounting >= 48)
                    {
                        if (xmaiId == 1)
                        {
                            var xupdatemail = await _context.EmailSettings.AsNoTracking().FirstOrDefaultAsync(x => x.Id == 2);
                            if (xupdatemail.DateStart > DateTime.UtcNow.AddHours(1))
                            {
                                xupdatemail.Active = true;
                            }
                           
                            xupdatemail.Count = 0;
                            xupdatemail.DateStart = DateTime.UtcNow.AddHours(2);
                            _context.Attach(xupdatemail).State = EntityState.Modified;
                        }
                        else if (xmaiId == 2)
                        {
                            var xupdatemail = await _context.EmailSettings.AsNoTracking().FirstOrDefaultAsync(x => x.Id == 3);
                            if (xupdatemail.DateStart > DateTime.UtcNow.AddHours(1))
                            {
                                xupdatemail.Active = true;
                            }
                            xupdatemail.Count = 0;
                            xupdatemail.DateStart = DateTime.UtcNow.AddHours(1);
                            _context.Attach(xupdatemail).State = EntityState.Modified;
                        }
                        else if (xmaiId == 3)
                        {
                            var xupdatemail = await _context.EmailSettings.AsNoTracking().FirstOrDefaultAsync(x => x.Id == 1);
                            if (xupdatemail.DateStart > DateTime.UtcNow.AddHours(1))
                            {
                                xupdatemail.Active = true;
                            }
                            xupdatemail.Count = 0;
                            xupdatemail.DateStart = DateTime.UtcNow.AddHours(1);
                            _context.Attach(xupdatemail).State = EntityState.Modified;
                        }
                        

                    }
                   
                    i.SentVia = xmail;
                }
                else if (i.NotificationType == NotificationType.SMS)
                {
                    //
                    //string result = await SendSms(i.Recipient, i.Mail);
                    string result = "";
                    if (result.Contains("OK"))
                    {
                        i.NotificationStatus = NotificationStatus.Sent;
                    }
                    else
                    {
                        i.NotificationStatus = NotificationStatus.NotSent;
                        i.Retries = i.Retries + 1;
                    }

                }

                try
                {

                    var iod = await _context.Messages.AsNoTracking().FirstOrDefaultAsync(x => x.Id == i.Id);
                    iod.NotificationStatus = i.NotificationStatus;
                    iod.Retries = i.Retries;
                    _context.Attach(iod).State = EntityState.Modified;



                }

                catch (Exception webex)
                {

                }

                await _context.SaveChangesAsync();
            }
            await _context.SaveChangesAsync();
        }
    }
}
