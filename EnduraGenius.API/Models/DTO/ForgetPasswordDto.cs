using System.ComponentModel.DataAnnotations;

namespace EnduraGenius.API.Models.DTO
{
    /// <summary>
    /// DTO to handle the data needed to send the reset pass email
    /// </summary>
    public class ForgetPasswordDto
    {
        /// <summary>
        /// email of the user
        /// </summary>
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        /// <summary>
        /// the client uri to send the email to
        /// </summary>
        [Required]
        public string ClientURI { get; set; }
    }
}
