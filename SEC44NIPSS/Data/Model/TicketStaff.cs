using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SEC44NIPSS.Data.Model
{
    public class TicketStaff
    {
        public long Id { get; set; }
        public string WorkCarriedOut { get; set; }
        public string Signature { get; set; }
        public TicketStaffStatus TicketStaffStatus { get; set; }
        public long ProfileId { get; set; }
        public Profile Profile { get; set; }
        public DateTime Date { get; set; }
        

        public long TicketId { get; set; }
        public Ticket Ticket { get; set; }
    }
}
