using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SEC44NIPSS.Data.Model
{
    public class Tour
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }

        public long? AlumniId { get; set; }
        public Alumni Alumni { get; set; }

        public long? StudyGroupId { get; set; }
        public StudyGroup StudyGroup { get; set; }
    }
}
