using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SEC44NIPSS.Data.Model
{
    public class Event
    {
        public long Id { get; set; }
        public DateTime Date { get; set; }
        public string EventDate { get; set; }
        public string EventTime { get; set; }
        public int SortOrder { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Moderator { get; set; }
        public string Lecturer { get; set; }
        public EventType EventType { get; set; }
        public ContentStatus ContentStatus { get; set; }
        public ContentType ContentType { get;set;}
        public string Note { get; set; }
        public bool IsLecture { get; set; }
        public string Module { get; set; }

        public virtual Document Document { get; set; }
    }
}
