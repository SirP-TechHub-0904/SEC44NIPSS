using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SEC44NIPSS.Data.Model
{
    public class ParlyReportCategory
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string BgColor { get; set; }
        public DateTime Date { get; set; }
        public int SortOrder { get; set; }
        public bool Show { get; set; }
        public FolderType FolderType { get; set; }

        public ICollection<ParlyReportSubCategory> ParlyReportSubCategories { get; set; }
        public ICollection<ParlyReportDocument> ParlyReportDocuments { get; set; }
    }
}
