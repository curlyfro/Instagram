using System;
using System.Collections.Generic;
using System.Text;

namespace Instagram.ViewModels.ProfileViewModels
{
    public class BaseUserViewModel
    {
        private const string DefaultAvatarPath= @"/files/default.png";

        public string UserId { get; set; }
        public string MainPhotoPath { get; set; } = DefaultAvatarPath;
        public string UserName { get; set; }
    }
}
