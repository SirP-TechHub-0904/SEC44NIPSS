using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SEC44NIPSS.Data.Model
{
    public class DocumentCategory
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public string Color { get; set; }
      
        public long? AlumniId { get; set; }
        public Alumni Alumni { get; set; }
        public ICollection<Document> Documents { get; set; }
        public ICollection<SecPaper> SecPapers { get; set; }
        public bool Show { get; set; }
    }
}
