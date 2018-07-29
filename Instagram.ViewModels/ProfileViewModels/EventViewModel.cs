using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Instagram.ViewModels.ProfileViewModels
{
    public class EventViewModel
    {
        public BaseUserViewModel User { get; set; }
        public string Message { get; set; }
        public PostViewModel Post { get; set; }
        public DateTime EventDate { get; set; }
    }
}
