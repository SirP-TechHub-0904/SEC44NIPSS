using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SEC44NIPSS.Data.Model
{
    public class TicketSupervisor
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public bool SendEmail { get; set; }
        public bool SendPhone { get; set; }
        public int Set { get; set; }
    }
}
