using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SEC44NIPSS.Data.Model
{
    public class Comment
    {
        public long Id { get; set; }
        public string Content { get; set; }
        public DateTime Date { get; set; }
        public string UserId { get; set; }
        public string CommentBy { get; set; }
        public IdentityUser User { get; set; }

        public long NewsId { get; set; }
        public News News { get; set; }
    }
}
