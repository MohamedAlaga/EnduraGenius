
using EnduraGenius.API.Models.Domain;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;

namespace EnduraGenius.API.Repositories.EmailSenderRepository
{
    public class EmailSender : IEmailSender
    {
        private readonly IOptions<EmailSettingsModel> _emailSettings;
        public EmailSender(IOptions<EmailSettingsModel> emailSettings)
        {
            _emailSettings = emailSettings;
        }

        public Task SendEmailAsync(string emailReciver, string subject, string message)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(this._emailSettings.Value.email));
            email.To.Add(MailboxAddress.Parse(emailReciver));
            email.Subject = subject;
            email.Body = new TextPart(TextFormat.Html)
            {
                Text = message
            };
            using var smtp = new SmtpClient();
            smtp.Connect(this._emailSettings.Value.host, this._emailSettings.Value.port, SecureSocketOptions.StartTls);
            smtp.Authenticate(this._emailSettings.Value.email, this._emailSettings.Value.password);
            smtp.Send(email);
            smtp.Disconnect(true);
            return Task.CompletedTask;
        }
    }
}
