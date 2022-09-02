using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SEC44NIPSS.Data.Model
{
    public class UserAnswer
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public string Score { get; set; }
        public int QuestionNumber { get; set; }
        public string CombineCode { get; set; }
        public DateTime? StartTime { get; set; }
        public long? ProfileId { get; set; }
        public Profile Profile { get; set; }

        public bool QuestionsLoaded { get; set; }
        public ICollection<AnswerList> AnswerLists { get; set; }
    }
}
