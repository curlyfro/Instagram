using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace Instagram.Services.Extensions
{
    public static class ResponseExtensions
    {
        public static void AddApplicationError(this HttpResponse response, string message)
        {
            response.Headers.Add("Application-Error", message);
            response.Headers.Add("access-control-expose-headers", "Application-Error");
        }
    }
}
