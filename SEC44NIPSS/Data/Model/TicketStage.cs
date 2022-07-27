using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SEC44NIPSS.Data.Model
{
    public class TicketStage
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public long TicketId { get; set; }
        public Ticket Ticket { get; set; }
    }
}
