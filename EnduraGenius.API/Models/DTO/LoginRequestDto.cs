using System.ComponentModel.DataAnnotations;

namespace EnduraGenius.API.Models.DTO
{
    /// <summary>
    /// DTO to handle the data needed to login
    /// </summary>
    public class LoginRequestDto
    {
        /// <summary>
        /// the email of the user
        /// </summary>
        [Required]
        [DataType(DataType.EmailAddress)]
        public string EmailAdress { get; set; }

        /// <summary>
        /// the password of the user
        /// </summary>
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Password { get; set; }
    }
}
