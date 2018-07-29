using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Instagram.DataAccess;
using Instagram.Domain;
using Instagram.ViewModels.ProfileViewModels;

namespace Instagram.Queries
{
    public class FollowersReader
    {
        private readonly ApplicationDbContext _context;

        public FollowersReader(
            ApplicationDbContext context)
        {
            _context = context;
        }

        public List<BaseUserViewModel> GetFollowers(ApplicationUser user)
        {
            var userFollowers = (from f in _context.Followers
                where f.FollowingUser == user
                select f.FollowerUser).ToList();

            var followers = new List<BaseUserViewModel>();

            foreach (var follower in userFollowers)
            {
                var viewModel = new BaseUserViewModel
                {
                    UserId = follower.Id,
                    MainPhotoPath = follower.MainPhotoPath,
                    UserName = follower.UserName
                };

                followers.Add(viewModel);
            }

            return followers;
        }
    }
}
