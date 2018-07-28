using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Instagram.Domain
{
    public class Comment
    {
        public Guid CommentId { get; set; }
        public Post Post { get; set; }
        public ApplicationUser User { get; set; }
        public string Content { get; set; }
        public DateTime Date { get; set; }

        public Comment() : base()
        {
        }
    }
}
