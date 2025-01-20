using System.ComponentModel.DataAnnotations;

namespace EnduraGenius.API.Models.DTO
{
    /// <summary>
    /// DTO to handle the user profile response
    /// </summary>
    public class UserProfileResponseDTO
    {
        /// <summary>
        /// user name
        /// </summary>
        [Required]
        public string UserName { get; set; }

        /// <summary>
        /// user email
        /// </summary>
        [Required]
        public string Email { get; set; }

        /// <summary>
        /// user age
        /// </summary>
        [Required]
        public int Age { get; set; }

        /// <summary>
        /// user weight in kg
        /// </summary>
        [Required]
        public float WeightInKg { get; set; }
        /// <summary>
        /// user height in cm
        /// </summary>
        [Required]
        public int TallInCm { get; set; }
        /// <summary>
        /// true if the
        /// </summary>
        [Required]
        public bool IsMale { get; set; }
        /// <summary>
        /// the user points
        /// </summary>
        [Required]
        public int Points { get; set; }
        /// <summary>
        /// the user profile picture
        /// </summary>
        [Required]
        public string? ProfilePicture { get; set; }
        /// <summary>
        /// the user workouts
        /// </summary>
        [Required]
        public List<UserWorkoutResponseDTO> userWorkouts { get; set; }
    }
}
