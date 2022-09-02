using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SEC44NIPSS.Data.Model
{
    public class Executive
    {
        public long Id { get; set; }
        public string Position { get; set; }
        public string SortOrder { get; set; }
        public string Image { get; set; }
        public string Manifesto { get; set; }
        public long? AlumniId { get; set; }
        public Alumni Alumni { get; set; }

        public long? ProfileId { get; set; }
        public Profile Profile { get; set; }

        public long? ParticipantId { get; set; }
        public SecParticipant Participant { get; set; }
    }
}
