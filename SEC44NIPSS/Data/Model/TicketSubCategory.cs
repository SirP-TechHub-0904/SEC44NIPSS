using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SEC44NIPSS.Data.Model
{
    public class TicketSubCategory
    {
        public long Id { get; set; }
        public string Title { get; set; }

        public long TicketCategoryId { get; set; }
        public TicketCategory TicketCategory { get; set; }
    }
}
