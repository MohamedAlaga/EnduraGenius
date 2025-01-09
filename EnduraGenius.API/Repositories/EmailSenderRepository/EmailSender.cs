
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Http.HttpResults;
using MimeKit;
using MimeKit.Text;

namespace EnduraGenius.API.Repositories.EmailSenderRepository
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string emailReciver, string subject, string message)
        {
            var email = new MimeMessage();
            var myemail = "callie.feest@ethereal.email";
            email.From.Add(MailboxAddress.Parse(myemail));
            email.To.Add(MailboxAddress.Parse(emailReciver));
            email.Subject = subject;
            email.Body = new TextPart(TextFormat.Html)
            {
                Text = message
            };
            using var smtp = new SmtpClient();
            smtp.Connect("smtp.ethereal.email", 587, SecureSocketOptions.StartTls);
            smtp.Authenticate(myemail, "SnA8uaXwBCK5wCW8f8");
            smtp.Send(email);
            smtp.Disconnect(true);
            return Task.CompletedTask;
        }
    }
}
