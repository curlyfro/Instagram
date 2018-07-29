using Instagram.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using Instagram.Domain;
using Microsoft.EntityFrameworkCore;
using Instagram.ViewModels.ProfileViewModels;
using Microsoft.AspNetCore.Identity;

namespace Instagram.Queries
{
    public class SearchedPostsReader
    {
        private readonly ApplicationDbContext _context;

        public SearchedPostsReader(
            ApplicationDbContext context)
        {
            _context = context;
        }

        public List<PostViewModel> GetSearchedPosts(string tagAsString, ApplicationUser currentUser)
        {
            var tag = _context.Hashtags
                .SingleOrDefault(p => p.Name == tagAsString);

            var posts = _context.PostTags
                .Where(t => t.Tag == tag)
                .Select(p => p.Post)
                .Include(p => p.Likes)
                .Include(p => p.Creator)
                .ToList();

            var postsToReturn = new List<PostViewModel>();

            foreach (var post in posts.OrderByDescending(p => p.DateOfPublication))
            {
                var postView = new PostViewModel
                {
                    PostId = post.PostId,
                    UserName = post.Creator.UserName,
                    PhotoPath = post.PicturePath,
                    Description = post.Description,
                    DateOfPublication = post.DateOfPublication,
                    LikesQuantity = post.Likes.Count,
                    IsLiked = post.Likes.Any(p => p.User == currentUser)
                };

                postView.ReplaceDescription(postView.Description);
                postsToReturn.Add(postView);
            }

            return postsToReturn;
        }
    }
}
