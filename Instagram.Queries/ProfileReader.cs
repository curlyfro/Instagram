using Instagram.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Instagram.Domain;
using Instagram.ViewModels.ProfileViewModels;
using Microsoft.EntityFrameworkCore;

namespace Instagram.Queries
{
    public class ProfileReader
    {
        private readonly ApplicationDbContext _context;

        public ProfileReader(
            ApplicationDbContext context)
        {
            _context = context;
        }

        public ProfileViewModel GetProfile(string userId, ApplicationUser currentUser)
        {
            var user = _context.Users.Single(p => p.Id == userId);

            var posts = _context.Posts
                .Where(p => p.Creator == user)
                .OrderByDescending(p => p.DateOfPublication)
                .Include(p=>p.Likes)
                .ToList();

            var profile = new ProfileViewModel
            {
                UserId = user.Id,
                UserName = user.UserName,
                FullName = user.FullName,
                ProfileDesc = user.ProfileDesc
            };

            foreach (var post in posts)
            {
                var postView = new PostViewModel
                {
                    PostId = post.PostId,
                    UserName = post.Creator.UserName,
                    PhotoPath = post.PicturePath,
                    Description = post.Description,
                    DateOfPublication = post.DateOfPublication,
                    LikesQuantity = post.Likes.Count,
                    IsLiked = post.Likes.Any(p=>p.User==currentUser)
                };
                postView.ReplaceDescription(postView.Description);
                
                profile.Posts.Add(postView);
            }

            return profile;
        }
    }
}
