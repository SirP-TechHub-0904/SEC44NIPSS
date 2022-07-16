using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SEC44NIPSS.Data.Model
{
    public class LegacyProjectAnswer
    {
        public long Id { get; set; }
        public string Email { get; set; }
        public string Answer { get; set; }
        public string Message { get; set; }

        public VotingType VotingType { get; set; }
    }
}
