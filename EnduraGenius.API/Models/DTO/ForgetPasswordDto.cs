using System.ComponentModel.DataAnnotations;

namespace EnduraGenius.API.Models.DTO
{
    public class ForgetPasswordDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string ClientURI { get; set; }
    }
}
