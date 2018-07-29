using MimeKit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MailKit.Net.Smtp;

namespace Instagram.Services.Senders
{
    public class EmailSender : IEmailSender
    {
        public async Task SendEmailAsync(string email, string subject, string message)
        {
            string appEmail = "testinstaapp100@gmail.com";
            string emailPassword = "12345test";
            string fromWho = "Site administration";
            string smtp = "smtp.gmail.com";
            int port = 25;
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress(fromWho, appEmail));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message
            };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(smtp, port, false);
                await client.AuthenticateAsync(appEmail, emailPassword);
                await client.SendAsync(emailMessage);

                await client.DisconnectAsync(true);
            }
        }
    }
}
