using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SEC44NIPSS.Data.Model
{
    public class TicketCategory
    {
        public long Id { get; set; }
        public string Title { get; set; }

        public ICollection<TicketSubCategory> TicketSubCategories { get; set; }
    }
}
