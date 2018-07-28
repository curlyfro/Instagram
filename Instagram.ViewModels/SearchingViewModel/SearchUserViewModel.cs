using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Instagram.ViewModels.ProfileViewModels;

namespace Instagram.ViewModels.SearchingViewModel
{
    public class SearchUserViewModel
    {
        [Display(Name = "Enter username or full name")]
        public string Name { get; set; }

        public ICollection<BaseUserViewModel> Users { get; set; }

        public SearchUserViewModel()
        {
            Users = new List<BaseUserViewModel>();
        }
    }
}
