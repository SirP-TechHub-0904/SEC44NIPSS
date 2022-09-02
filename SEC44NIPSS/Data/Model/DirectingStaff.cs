using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SEC44NIPSS.Data.Model
{
    public class DirectingStaff
    {
        public long Id { get; set; }
        public string SortOrder { get; set; }

        public long? AlumniId { get; set; }
        public Alumni Alumni { get; set; }

        public long? ProfileId { get; set; }
        public Profile Profile { get; set; }

        public long? StudyGroupId { get; set; }
        public StudyGroup StudyGroup { get; set; }

    }
}
