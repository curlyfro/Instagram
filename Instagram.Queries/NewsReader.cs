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
    public class NewsReader
    {
        private readonly ApplicationDbContext _context;

        public NewsReader(
            ApplicationDbContext context)
        {
            _context = context;
        }

        public List<PostViewModel> GetNews(ApplicationUser currentUser)
        {
            var userFollowings = (from f in _context.Followers
                where f.FollowerUser == currentUser
                select f.FollowingUser).ToList();

            var posts = _context.Posts
                .Where(p => p.Creator == currentUser)
                .OrderByDescending(p => p.DateOfPublication)
                .Include(p=>p.Creator)
                .Include(p=>p.Likes)
                .ToList();

            foreach (var profile in userFollowings)
            {
                var profilePosts = _context.Posts.Where(p => p.Creator == profile);
                posts.AddRange(profilePosts);
            }

            var postsToReturn = new List<PostViewModel>();

            foreach (var post in posts)
            {
                var postViewModel = new PostViewModel
                {
                    PostId = post.PostId,
                    UserName = post.Creator.UserName,
                    PhotoPath = post.PicturePath,
                    Description = post.Description,
                    DateOfPublication = post.DateOfPublication,
                    LikesQuantity = post.Likes.Count,
                    IsLiked = post.Likes.Any(p=>p.User==currentUser)
                };
                postViewModel.ReplaceDescription(postViewModel.Description);

                postsToReturn.Add(postViewModel);
            }

            return postsToReturn;
        }
    }
}
