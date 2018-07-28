using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Instagram.Domain
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
        public string ProfileDesc { get; set; }
        public string MainPhotoPath { get; set; }
        public ICollection<Post> Posts { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<Like> Likes { get; set; }
        public ICollection<Follower> Followers { get; set; }
        public ICollection<Follower> Followings { get; set; }

        public ApplicationUser() : base()
        {
            Posts = new List<Post>();
            Comments = new List<Comment>();
            Likes = new List<Like>();
            Followers = new List<Follower>();
            Followings = new List<Follower>();
        }
    }
}
