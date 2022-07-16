using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SEC44NIPSS.Data.Model
{
    public class Gallery
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string FilePath { get; set; }
        public bool DontShow { get; set; }
        public bool Private { get; set; }
        public DateTime Date { get; set; }

        public bool UseAsActivity { get; set; }
        public long? ProfileId { get; set; }
        public Profile Profile { get; set; }

        public string Tag { get; set; }
    }
}
