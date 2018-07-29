using System;
using System.Collections.Generic;
using System.Text;

namespace Instagram.Domain
{
    public class Follower
    {
        public Guid Id { get; set; }
        public ApplicationUser FollowingUser { get; set; }
        public ApplicationUser FollowerUser { get; set; }
        public DateTime DateOfFollowing { get; set; }
    }
}
