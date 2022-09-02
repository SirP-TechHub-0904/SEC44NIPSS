using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SEC44NIPSS.Data.Model
{
    public class ParticipantDocumentCategory
    {
        public long Id { get; set; }
        public string Title { get; set; }

        public ICollection<ParticipantDocument> ParticipantDocuments { get; set; }
    }
}
