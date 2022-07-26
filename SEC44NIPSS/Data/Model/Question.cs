using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SEC44NIPSS.Data.Model
{
    public class Question
    {

        public long Id { get; set; }
        public string Title { get; set; }
        public string ImageUrl { get; set; }
        public Option Options { get; set; }
        public int Number { get; set; }
        public int PageNumber { get; set; }
        public int SortOrder { get; set; }
        public string Required { get; set; }

        public long QuestionnerId { get; set; }
        public Questionner Questionner { get; set; }

    }
}
