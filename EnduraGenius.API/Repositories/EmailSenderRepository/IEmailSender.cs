namespace EnduraGenius.API.Repositories.EmailSenderRepository
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
