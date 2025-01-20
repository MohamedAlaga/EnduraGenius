namespace EnduraGenius.API.Repositories.EmailSenderRepository
{
    /// <summary>
    /// Interface for the email sender service
    /// </summary>
    public interface IEmailSender
    {
        /// <summary>
        /// Send an email to the user
        /// </summary>
        /// <param name="email">the email address of the user</param>
        /// <param name="subject">the subject of the email</param>
        /// <param name="message">the content of the email</param>
        /// <returns>
        /// does not return anything
        /// </returns>
        Task SendEmailAsync(string email, string subject, string message);
    }
}
