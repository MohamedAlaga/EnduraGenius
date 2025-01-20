using System.ComponentModel.DataAnnotations;

namespace EnduraGenius.API.Models.DTO
{
    /// <summary>
    /// DTO to handle the data needed to send the user workout response
    /// </summary>
    public class UserWorkoutResponseDTO
    {
        /// <summary>
        /// the name of the workout
        /// </summary>
        [Required]
        public string Name { get; set; }
        /// <summary>
        /// the descreption of the workout
        /// </summary>
        [Required]
        public string Descreption { get; set; }
        /// <summary>
        /// the max weight the user can perform
        /// </summary>
        [Required]
        public float MaxWeight { get; set; }
        /// <summary>
        /// the last weight the user performed
        /// </summary>
        [Required]
        public int LastWeight { get; set; }
        /// <summary>
        /// the times the user performed the workout
        /// </summary>
        [Required]
        public int TimesPerformed { get; set; }
    }
}
