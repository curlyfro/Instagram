using System;
using System.Collections.Generic;
using System.Text;

namespace InstaApp.DTO.ViewModels
{
    public class ErrorViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
