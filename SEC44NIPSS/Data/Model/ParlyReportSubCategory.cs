using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SEC44NIPSS.Data.Model
{
    public class ParlyReportSubCategory
    {
        public ParlyReportSubCategory()
        {
            Date = DateTime.UtcNow;
        }
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public int SortOrder { get; set; }
        public string BgColor { get; set; }


        public long? ParlyReportCategoryId { get; set; }
        public ParlyReportCategory ParlyReportCategory {get;set;}
        public ICollection<ParlyReportDocument> ParlyReportDocuments { get; set; }
        public ICollection<ParlySubTwoCategory> ParlySubTwoCategories { get; set; }
    }
}
