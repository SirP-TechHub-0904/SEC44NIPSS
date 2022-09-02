using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SEC44NIPSS.Data.Model
{
    public class Committee
    {
        public long Id { get; set; }
        public string Position { get; set; }
        public string SortOrder { get; set; }

        public long CommitteeCategoryId { get; set; }
        public CommitteeCategory CommitteeCategory { get; set; }
        public long? ProfileId { get; set; }
        public Profile Profile { get; set; }

        public long? AlumniId { get; set; }
        public Alumni Alumni { get; set; }
    }
}
