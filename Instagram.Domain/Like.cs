using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Instagram.Domain
{
    public class Like
    {
        public Guid LikeId { get; set; }
        public ApplicationUser User { get; set; }
        public Post Post { get; set; }
        public DateTime Date { get; set; }

        public Like()
        {
        }
    }
}
