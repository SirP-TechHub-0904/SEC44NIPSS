using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SEC44NIPSS.Data.Model
{
    public class Document
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }

        public long DocumentCategoryId { get; set; }
        public DocumentCategory DocumentCategory { get; set; }
        public long? EventId { get; set; }
        public Event Event { get; set; }
        public long ProfileId { get; set; }
        public Profile Profile { get; set; }

        public string CoverImage { get; set; }
        public string FileName { get; set; }
        public bool Uploaded { get; set; }
        public DocType DocType { get; set; }
        public bool DontShow { get; set; }

    }
}
