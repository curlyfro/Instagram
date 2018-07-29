using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Instagram.DataAccess;
using Instagram.Domain;
using Instagram.ViewModels.ProfileViewModels;

namespace Instagram.Queries
{
    public class PostReader
    {
        private readonly ApplicationDbContext _context;

        public PostReader(
            ApplicationDbContext context)
        {
            _context = context;
        }

        public PostViewModel GetPost(Guid postId, ApplicationUser currentUser)
        {
            var post = _context.Posts
                .Single(p => p.PostId == postId);

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

            return postViewModel;
        }
    }
}
