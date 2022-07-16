using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SEC44NIPSS.Data.Model
{
    public class CurrentAffair
    {
        public long Id { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public string FullContent { get; set; }
        public DateTime Date { get; set; }
    }
}
