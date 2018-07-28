using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Instagram.Domain
{
    public class Post
    {
        public Guid PostId { get; set; }
        public string PicturePath { get; set; }
        public string Description { get; set; }
        public ApplicationUser Creator { get; set; }
        public DateTime DateOfPublication { get; set; }
        public ICollection<PostTag> PostTags { get; set; }
        public ICollection<Like> Likes { get; set; }
        public ICollection<Comment> Comments { get; set; }

        public Post()
        {
            PostTags = new List<PostTag>();
            Likes = new List<Like>();
            Comments = new List<Comment>();
        }
    }
}
