using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SEC44NIPSS.Data.Dtos
{
    public class NipssStaffListDto
    {
        public long Id { get; set; }
        public string Position { get; set; }
        public string Fullname { get; set; }
        public string Photo { get; set; }
    }
}
