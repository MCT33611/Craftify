using Microsoft.AspNetCore.Identity.UI.Services;
using MimeKit;

namespace Craftify.Infrastructure.Services
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var emailToSend = new MimeMessage();

            emailToSend.From.Add(MailboxAddress.Parse("craftify.onion0.122@gmail.com"));
            emailToSend.To.Add(MailboxAddress.Parse(email));
            emailToSend.Subject = subject;
            emailToSend.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = htmlMessage };

            //Send email

            using (var emailClient = new MailKit.Net.Smtp.SmtpClient())
            {
                emailClient.Connect("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
                emailClient.Authenticate("craftify.onion0.122@gmail.com", "vjyr zozq urep wywz");
                emailClient.Send(emailToSend);
                emailClient.Disconnect(true);

            }

            return Task.CompletedTask;
        }
    }
}
