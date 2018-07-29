using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Instagram.Services.Senders
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
