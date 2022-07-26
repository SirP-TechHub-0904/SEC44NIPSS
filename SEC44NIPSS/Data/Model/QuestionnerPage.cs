using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SEC44NIPSS.Data.Model
{
    public class QuestionnerPage
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Instruction { get; set; }
        public int Number { get; set; }

        public long QuestionnerId { get; set; }
        public Questionner Questionner { get; set; }
    }
}
