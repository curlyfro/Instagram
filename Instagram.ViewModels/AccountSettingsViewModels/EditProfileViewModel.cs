using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace Instagram.ViewModels.AccountSettingsViewModels
{
    public class EditProfileViewModel
    {
        public string CurrentMainPhotoPath { get; set; }

        public IFormFile NewPhoto { get; set; }

        public string Username { get; set; }

        public string FullName { get; set; }

        [Display(Name = "Profile description")]
        public string ProfileDesc { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }

        public string StatusMessage { get; set; }
    }
}
