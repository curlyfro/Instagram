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
    public class EventsReader
    {
        private readonly ApplicationDbContext _context;

        public EventsReader(
            ApplicationDbContext context)
        {
            _context = context;
        }

        public List<EventViewModel> GetEvents(ApplicationUser user)
        {
            var allPosts = _context.Posts
                .Where(p => p.Creator == user)
                .Include(p => p.Likes)
                .ToList();

            var likes = new List<Like>();

            foreach (var post in allPosts)
            {
                var likesToAdd = post.Likes;

                likes.AddRange(likesToAdd);
            }

            var followers= (from f in _context.Followers
                where f.FollowingUser == user
                select new
                {
                    f.FollowerUser,
                    f.DateOfFollowing
                }).ToList();

            var events = new List<EventViewModel>();

            foreach (var like in likes)
            {
                var userViewModel = new BaseUserViewModel
                {
                    UserId = like.User.Id,
                    UserName = like.User.UserName,
                    MainPhotoPath = like.User.MainPhotoPath
                };

                var postViewModel = new PostViewModel
                {
                    PostId = like.Post.PostId,
                    PhotoPath = like.Post.PicturePath
                };

                var eventViewModel = new EventViewModel
                {
                    User=userViewModel,
                    EventDate = like.Date,
                    Message = "liked your post",
                    Post = postViewModel
                };

                events.Add(eventViewModel);
            }

            foreach (var follower in followers)
            {
                var userViewModel = new BaseUserViewModel
                {
                    UserId = follower.FollowerUser.Id,
                    UserName = follower.FollowerUser.UserName,
                    MainPhotoPath = follower.FollowerUser.MainPhotoPath
                };
                
                var eventViewModel = new EventViewModel
                {
                    User = userViewModel,
                    EventDate = follower.DateOfFollowing,
                    Message = "followed you"
                };

                events.Add(eventViewModel);
            }

            var eventsToReturn= events.OrderByDescending(p => p.EventDate).ToList();
            return eventsToReturn;
        }
    }
    
}
