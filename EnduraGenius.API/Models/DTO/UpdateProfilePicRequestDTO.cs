using System.ComponentModel.DataAnnotations;

namespace EnduraGenius.API.Models.DTO
{
    public class UpdateProfilePicRequestDTO
    {
        [Required]
        public IFormFile newPicture { get; set; }
    }
}
