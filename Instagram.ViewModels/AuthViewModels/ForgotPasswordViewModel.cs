using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Instagram.ViewModels.AuthViewModels
{
    public class ForgotPasswordViewModel
    {
        [Display(Name = "Enter your email")]
        public string Email { get; set; }
    }
}
