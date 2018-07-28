using System;
using System.Collections.Generic;
using System.Text;

namespace Instagram.Domain
{
    public class Hashtag
    {
        public Guid HashtagId { get; set; }
        public ICollection<PostTag> PostTags { get; set; }
        public string Name { get; set; }

        public Hashtag()
        {
            PostTags = new List<PostTag>();
        }
    }
}
