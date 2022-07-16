using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SEC44NIPSS.Data.Model
{
    public class NipssStaff
    {
        public long Id { get; set; }
        public string Position { get; set; }
        public string SortOrder { get; set; }
        public string Manifesto { get; set; }
        public bool IsExecutive { get; set; }

        public long? ProfileId { get; set; }
        public Profile Profile { get; set; }
    }
}
