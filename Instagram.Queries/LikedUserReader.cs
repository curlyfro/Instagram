using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Instagram.DataAccess;
using Instagram.Domain;
using Instagram.ViewModels.ProfileViewModels;

namespace Instagram.Queries
{
    public class LikedUserReader
    {
        private readonly ApplicationDbContext _context;

        public LikedUserReader(
            ApplicationDbContext context)
        {
            _context = context;
        }

        public List<BaseUserViewModel> GetLikes(Guid postId)
        {
            var likes = _context.Likes
                .Where(p => p.Post.PostId == postId).ToList();

            var users = new List<ApplicationUser>();

            foreach (var like in likes)
            {
                var user = _context.Users
                    .SingleOrDefault(p => p.Likes.Contains(like));

                users.Add(user);
            }

            var usersViewModel = new List<BaseUserViewModel>();

            foreach (var user in users)
            {
                var userViewModel = new BaseUserViewModel()
                {
                    MainPhotoPath = user.MainPhotoPath,
                    UserName = user.UserName,
                };

                usersViewModel.Add(userViewModel);
            }

            return usersViewModel;
        }
    }
}
