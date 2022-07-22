using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SEC44NIPSS.Data.Model
{
    public class TicketRequirement
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public int QuantityRequired { get; set; }
        public int QuantityIssued { get; set; }
        public decimal Cost { get; set; }
        public string SIVno { get; set; }
        public string Image { get; set; }
        public DateTime Date { get; set; }

        public long TicketId { get; set; }
        public Ticket Ticket { get; set; }
    }
}
