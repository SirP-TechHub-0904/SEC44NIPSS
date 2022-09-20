using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SEC44NIPSS.Data.Model
{
    public class ParlyReportDocument
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Document { get; set; }
        public string ShortDescription { get; set; }
        public string Note { get; set; }
        public DateTime Date { get; set; }

        public long? ProfileId { get; set; }
        public Profile Profile { get; set; }

        public long? ParlyReportCategoryId { get; set; }
        public ParlyReportCategory ParlyReportCategory { get; set; }

        public long? ParlyReportSubCategoryId { get; set; }
        public ParlyReportSubCategory ParlyReportSubCategory { get; set; }
    }
}
