using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Instagram.DataAccess;
using Instagram.Domain;
using Instagram.ViewModels.ProfileViewModels;
using Microsoft.EntityFrameworkCore;

namespace Instagram.Queries
{
    public class ExploreReader
    {
        private readonly ApplicationDbContext _context;

        public ExploreReader(
            ApplicationDbContext context)
        {
            _context = context;
        }

        public List<PostViewModel> GetExplore(ApplicationUser currentUser)
        {
            var userFollowings = (from f in _context.Followers
                where f.FollowerUser == currentUser
                select f.FollowingUser).ToList();
            var users = new List<ApplicationUser>();

            foreach (var u in userFollowings)
            {
                var followings = (from f in _context.Followers
                    where f.FollowerUser == u
                    select f.FollowingUser).ToList();
                users.AddRange(followings);
            }

            var interestingUsers = users.Distinct().ToList();
            if (interestingUsers.Contains(currentUser))
            {
                interestingUsers.Remove(currentUser);
            }
            foreach (var u in userFollowings)
            {
                if (interestingUsers.Contains(u))
                {
                    interestingUsers.Remove(u);
                }
            }

            var posts = _context.Posts
                .Where(p => interestingUsers.Contains(p.Creator))
                .Include(p=>p.Likes)
                .Include(p=>p.Creator)
                .OrderByDescending(p => p.DateOfPublication)
                .ToList();
            
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
                    IsLiked = post.Likes.Any(p => p.User == currentUser)
                };
                postViewModel.ReplaceDescription(postViewModel.Description);

                postsToReturn.Add(postViewModel);
            }

            return postsToReturn;
        }
    }
}
