using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SEC44NIPSS.Data.Model
{
    public class SecProjectExecutive
    { 
        public long Id { get; set; }
        public long ProfileId { get; set; }
        public Profile Profile { get; set; }
        public string Position { get; set; }

        public long SecProjectId { get; set; }
        public SecProject SecProject { get; set; }

    }
}
