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
        

        public DocumentOwner DocumentOwner { get; set; }
        public long? ProfileId { get; set; }
        public Profile Profile { get; set; }

        public long? StudyGroupId { get; set; }
        public StudyGroup StudyGroup { get; set; }

        public long? AlumniId { get; set; } 
        public Alumni Alumni { get; set; }

        public long? EventId { get; set; }
        public Event Event { get; set; }

        public int SortOrder { get; set; }

        public long? ParlyReportCategoryId { get; set; }
        public ParlyReportCategory ParlyReportCategory { get; set; }

        public long? ParlyReportSubCategoryId { get; set; }
        public ParlyReportSubCategory ParlyReportSubCategory { get; set; }

        public long? ParlySubTwoCategoryId { get; set; }
        public ParlySubTwoCategory ParlySubTwoCategory { get; set; }

        public long? ParlySubThreeCategoryId { get; set; }
        public ParlySubThreeCategory ParlySubThreeCategory { get; set; }
    }
}

