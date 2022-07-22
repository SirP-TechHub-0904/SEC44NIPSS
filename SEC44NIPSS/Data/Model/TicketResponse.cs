using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SEC44NIPSS.Data.Model
{
    public class TicketResponse
    {
        public long Id { get; set; }
        public string Reply { get; set; }
        public string Image { get; set; }
        public DateTime CreatedTime { get; set; }

        public long TicketId { get; set; }
        public Ticket Ticket { get; set; }
    }
}
