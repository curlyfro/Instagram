using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Instagram.DataAccess;
using Instagram.Domain;
using Instagram.Services.Hashtag;
using Instagram.ViewModels.ProfileViewModels;
using Microsoft.AspNetCore.Hosting;
using Org.BouncyCastle.Asn1.Ocsp;

namespace Instagram.Commands
{
    public class CreatePostCommand
    {
        private readonly ApplicationDbContext _context;
        private readonly HashtagFinder _hashtagFinder;
        private readonly IHostingEnvironment _appEnvironment;

        public CreatePostCommand(
            ApplicationDbContext context,
            HashtagFinder hashtagFinder,
            IHostingEnvironment appEnvironment)
        {
            _context = context;
            _hashtagFinder = hashtagFinder;
            _appEnvironment = appEnvironment;
        }

        public void CreatePost(ApplicationUser currentUser, CreatePostViewModel viewModel)
        {
            string path = $"/files/{Guid.NewGuid()}_{viewModel.Photo.FileName}";

            using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
            {
                viewModel.Photo.CopyTo(fileStream);
            }

            Post post = new Post
            {
                Creator = currentUser,
                PicturePath = path,
                Description = viewModel.Description,
                DateOfPublication = DateTime.Now
            };

            _context.Posts.Add(post);
            _context.SaveChanges();
            _hashtagFinder.FindHashtags(post);
        }
    }
}