using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SEC44NIPSS.Data.Model
{
    public class Notification
    {
        public long Id { get; set; }
        public long UserToNotifyId { get; set; }
        public UserToNotify UserToNotify { get; set; }
        public string Message { get; set; }
        public DateTime DatetTime { get; set; }
        public string Title { get; set; }
        public string ImageUrl { get; set; }
        public string Link { get; set; }
        public bool Read { get; set; }
        public bool Sent { get; set; }
        public string Fullname { get; set; }
    }
}
