using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SEC44NIPSS.Data.Model
{
    public class UserToNotify
    {
        public long Id { get; set; }
        public long? ProfileId { get; set; }
        public Profile Profile { get; set; }
        public bool IsAndriod { get; set; }
        public string TokenId { get; set; }
        public ICollection<Notification> Notifications { get; set; }
    }
}
