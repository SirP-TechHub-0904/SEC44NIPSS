using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SEC44NIPSS.Data.Model
{
    public class SecPaper
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public long DocumentCategoryId { get; set; }
        public DocumentCategory DocumentCategory { get; set; }

        public string Powerpoint { get; set; }
        public string Report { get; set; }
        public string Script { get; set; }
        public long? AlumniId { get; set; }
        public Alumni Alumni { get; set; }
    }
}
