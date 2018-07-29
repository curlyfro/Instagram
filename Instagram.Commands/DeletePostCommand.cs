using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Instagram.DataAccess;
using Instagram.Domain;
using Instagram.Services.Hashtag;
using Instagram.ViewModels.ProfileViewModels;
using Microsoft.AspNetCore.Hosting;

namespace Instagram.Commands
{
    public class DeletePostCommand
    {
        private readonly ApplicationDbContext _context;
        private readonly IHostingEnvironment _appEnvironment;

        public DeletePostCommand(
            ApplicationDbContext context,
            IHostingEnvironment appEnvironment)
        {
            _context = context;
            _appEnvironment = appEnvironment;
        }

        public void Execute(Guid postId, ApplicationUser user)
        {
            var post = _context.Posts.SingleOrDefault(p => p.PostId == postId && p.Creator == user);

            if (post == null) { throw new Exception();}

            var postTags = _context.PostTags.Where(p => p.Post == post);
            var likes = _context.Likes.Where(p => p.Post == post);

            var fileInfo = new FileInfo(_appEnvironment.WebRootPath + post.PicturePath);
            fileInfo.Delete();

            _context.Likes.RemoveRange(likes);
            _context.PostTags.RemoveRange(postTags);
            _context.Posts.Remove(post);

            _context.SaveChanges();
        }
    }
}
