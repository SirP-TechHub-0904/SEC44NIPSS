using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SEC44NIPSS.Data.Model
{
    public class QuestionAnswer
    {
        public long Id { get; set; }
        public long? QuestionnerId { get; set; }
        public Questionner Questionner { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime Date { get; set; }

        public ICollection<QuestionResponse> QuestionResponses { get; set; }
    }
}
