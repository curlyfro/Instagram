using Instagram.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using Instagram.Domain;
using Microsoft.EntityFrameworkCore;

namespace Instagram.Commands
{
    public class PutLikeCommand
    {
        private readonly ApplicationDbContext _context;

        public PutLikeCommand(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Execute(Guid postId, ApplicationUser currentUser)
        {
            var post = _context.Posts
                .Include(p=>p.Likes)
                .Single(p => p.PostId == postId);

            if (post.Likes.Any(p=>p.User==currentUser))
            {
                var like = _context.Likes.Single(
                    p => p.User == currentUser && p.Post == post);
                _context.Likes.Remove(like);
                _context.SaveChanges();
            }
            else
            {
                var like = new Like
                {
                    User = currentUser,
                    Post = post,
                    Date = DateTime.Now
                };
                _context.Likes.Add(like);
                _context.SaveChanges();
            }
            
        }
    }
}
