using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SEC44NIPSS.Data.Model
{
    public class ParlySubThreeCategory
    {
        public ParlySubThreeCategory()
        {
            Date = DateTime.UtcNow;
        }
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public int SortOrder { get; set; }


        public long? ParlySubTwoCategoryId { get; set; }
        public ParlySubTwoCategory ParlySubTwoCategory { get;set;}
        public ICollection<ParlyReportDocument> ParlyReportDocuments { get; set; }
    }
}
