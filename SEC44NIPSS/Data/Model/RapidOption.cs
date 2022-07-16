using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SEC44NIPSS.Data.Model
{
    public class RapidOption
    {
        public long Id { get; set; }
        public string Option1 { get; set; }
        public string Option2 { get; set; }
        public string Option3 { get; set; }
        public string Option4 { get; set; }

        public string Answer { get; set; }

        public long RapidQuestionId { get; set; }
        public RapidQuestion RapidQuestion { get; set; }
    }
}
