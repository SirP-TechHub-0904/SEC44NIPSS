using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SEC44NIPSS.Data.Model
{
    public class TourPost
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }



        public long TourSubCategoryId { get; set; }
        public TourSubCategory TourSubCategory { get; set; }

        public string UserId { get; set; }
        public long TourPostTypeId { get; set; }
        public TourPostType TourPostType { get; set; }

        public string Photo { get; set; }
        public PostFileType PostFileType { get; set; }
    }
}
