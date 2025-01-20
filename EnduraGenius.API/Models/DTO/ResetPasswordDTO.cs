using System.ComponentModel.DataAnnotations;

namespace EnduraGenius.API.Models.DTO
{
    /// <summary>
    /// DTO to handle the data needed to reset the password
    /// </summary>
    public class ResetPasswordDTO
    {
        /// <summary>
        /// the new password of the user
        /// </summary>
        [Required(ErrorMessage ="Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        /// <summary>
        /// the confirmationof the new user password
        /// </summary>
        [Required(ErrorMessage = "Confirm Password is required")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password and Confirm Password do not match")]
        public string ConfirmPassword { get; set; }

        /// <summary>
        /// the token to reset the password
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// the email of the user
        /// </summary>
        public string Email { get; set; }
    }
}
