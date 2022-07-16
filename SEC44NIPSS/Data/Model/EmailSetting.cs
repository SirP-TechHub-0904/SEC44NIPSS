using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SEC44NIPSS.Data.Model
{
    public class EmailSetting
    {
        public long Id { get; set; }
        public string SenderEmail { get; set; }
        public string PX { get; set; }
        public int Count { get; set; }
        public DateTime DateStart { get; set; }
        public bool Active { get; set; }
        public bool Reset { get; set; }
    }
}
