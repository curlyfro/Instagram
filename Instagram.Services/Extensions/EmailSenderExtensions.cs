using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Instagram.Services.Senders;
using System.Text.Encodings.Web;

namespace Instagram.Services.Extensions
{
    public static class EmailSenderExtensions
    {
        public static Task SendEmailConfirmationAsync(this IEmailSender emailSender, string email, string link)
        {
            return emailSender.SendEmailAsync(email, "Confirm your email",
                $"Please confirm your account by clicking this link: <a href='{HtmlEncoder.Default.Encode(link)}'>link</a>");
        }

        public static Task SendSkippingPasswordAsync(this IEmailSender emailSender, string email, string link)
        {
            return emailSender.SendEmailAsync(email, "Insta password skipping", 
                $"Click the following link to sign in your account: <a href='{HtmlEncoder.Default.Encode(link)}'>link</a>");
        }
    }
}
