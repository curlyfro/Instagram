using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Instagram.DataAccess;
using Instagram.Domain;
using Instagram.ViewModels.ProfileViewModels;

namespace Instagram.Queries
{
    public class FollowingsReader
    {
        private readonly ApplicationDbContext _context;

        public FollowingsReader(
            ApplicationDbContext context)
        {
            _context = context;
        }

        public List<BaseUserViewModel> GetFollowers(ApplicationUser user)
        {
            var userFollowings = (from f in _context.Followers
                where f.FollowerUser == user
                select f.FollowingUser).ToList();
            var followings = new List<BaseUserViewModel>();

            foreach (var following in userFollowings)
            {
                var viewModel = new BaseUserViewModel
                {
                    UserId = following.Id,
                    MainPhotoPath = following.MainPhotoPath,
                    UserName = following.UserName
                };

                followings.Add(viewModel);
            }

            return followings;
        }
    }
}
