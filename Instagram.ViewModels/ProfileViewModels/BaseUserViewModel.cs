﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Instagram.ViewModels.ProfileViewModels
{
    public class BaseUserViewModel
    {
        public string UserId { get; set; }
        public string MainPhotoPath { get; set; }
        public string UserName { get; set; }
    }
}
