using System;
using System.Collections.Generic;
using System.Text;

namespace Instagram.ViewModels.ProfileViewModels
{
    public class ProfileViewModel:BaseUserViewModel
    {
        public string FullName { get; set; }

        public string ProfileDesc { get; set; }

        public ICollection<PostViewModel> Posts { get; set; }

        public ProfileViewModel()
        {
            Posts = new List<PostViewModel>();
        }
    }
}
