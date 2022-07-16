using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SEC44NIPSS.Data.Model
{
    public class ActivityPlanner
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public PlannerType PlannerType { get; set; }
        public RecurrenceType RecurrenceType { get; set; }
        public int RecurrentNumber { get; set; }
        public ReminderTime ReminderTime { get; set; }



        public long? MessageId { get; set; }
        public Message Message { get; set; }

        public long ProfileId { get; set; }
        public Profile Profile { get; set; }
    }
}
