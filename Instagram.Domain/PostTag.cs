using System;
using System.Collections.Generic;
using System.Text;

namespace Instagram.Domain
{
    public class PostTag
    {
        public Guid PostId { get; set; }
        public Post Post { get; set; }

        public Guid HashtagId { get; set; }
        public Hashtag Tag { get; set; }
    }
}
