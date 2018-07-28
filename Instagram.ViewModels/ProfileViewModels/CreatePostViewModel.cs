using Microsoft.AspNetCore.Http;
using System;

namespace Instagram.ViewModels.ProfileViewModels
{
    public class CreatePostViewModel
    {
        public IFormFile Photo { get; set; }

        public string Description { get; set; }

        public string StatusMessage { get; set; }
    }
}
