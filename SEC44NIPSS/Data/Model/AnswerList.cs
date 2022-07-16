using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SEC44NIPSS.Data.Model
{
    public class AnswerList
    {
        public long Id { get; set; }
        public long? RapidQuestionId { get; set; }
        public RapidQuestion RapidQuestion { get; set; }
        public string Choose { get; set; }
        public string Answer { get; set; }
        public int SN { get; set; }

        public long UserAnswerId { get; set; }
        public UserAnswer UserAnswer { get; set; }
    }
}
