using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SEC44NIPSS.Data.Model
{
    public class TourSubCategory
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public string Date { get; set; }
        public string Color { get; set; }
        public long TourCategoryId { get; set; }
        public TourCategory TourCategory { get; set; }

        public long? StudyGroupId { get; set; }
        public StudyGroup StudyGroup { get; set; }
        public ICollection<TourPost> TourPosts { get; set; }
    }
}
