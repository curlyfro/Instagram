using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Instagram.DataAccess;
using Instagram.Domain;
using Microsoft.EntityFrameworkCore;

namespace Instagram.Commands
{
    public class FollowCommand
    {
        private readonly ApplicationDbContext _context;

        public FollowCommand(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Execute(string userId, ApplicationUser currentUser)
        {
            var user = _context.Users.Single(p => p.Id == userId);
            var following = _context.Followers.SingleOrDefault(
                p => p.FollowingUser == user && p.FollowerUser == currentUser);
            
            if (following==null)
            {
                _context.Followers.Add(new Follower
                {
                    FollowerUser = currentUser,
                    FollowingUser = user
                });
                _context.SaveChanges();
            }
            else
            {
                _context.Followers.Remove(following);
                _context.SaveChanges();
            }
        }
    }
}
