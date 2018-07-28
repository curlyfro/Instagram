using System;
using System.Collections.Generic;
using System.Text;
using Instagram.ViewModels.ProfileViewModels;

namespace Instagram.ViewModels.SearchingViewModel
{
    public class SearchPostViewModel
    {
        public string SearchedTag { get; set; }

        public ICollection<PostViewModel> Posts { get; set; }

        public SearchPostViewModel()
        {
            Posts = new List<PostViewModel>();
        }
    }
}
