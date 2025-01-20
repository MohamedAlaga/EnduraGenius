using System.ComponentModel.DataAnnotations;

namespace EnduraGenius.API.Models.DTO
{
    /// <summary>
    /// DTO to handle the picture needed to update the profile picture
    /// </summary>
    public class UpdateProfilePicRequestDTO
    {
        /// <summary>
        /// the new picture
        /// </summary>
        [Required]
        public IFormFile newPicture { get; set; }
    }
}
