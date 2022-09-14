using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SEC44NIPSS.Data.Model
{
    public class ParticipantDocument
    {
        public long Id { get; set; }
        public long? ParticipantId { get; set; }
        public SecParticipant Participant { get; set; }

        public long? ParticipantDocumentCategoryId { get; set; }
        public ParticipantDocumentCategory ParticipantDocumentCategory { get; set; }

        public string Powerpoint { get; set; }
        public string Report { get; set; }
        public string Script { get; set; }
       
    }
}
