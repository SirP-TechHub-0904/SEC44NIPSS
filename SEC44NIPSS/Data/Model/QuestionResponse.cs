using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SEC44NIPSS.Data.Model
{
    public class QuestionResponse
    {
        public long Id { get; set; }
        public long? QuestionId { get; set; }
        public Question Question { get; set; }

        public string Answer { get; set; }
    }
}
