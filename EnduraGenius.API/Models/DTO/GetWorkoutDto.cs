using System.ComponentModel.DataAnnotations;

namespace EnduraGenius.API.Models.DTO
{
    /// <summary>
    /// DTO used to show the workout data to the user
    /// </summary>
    public class GetWorkoutDto
    {
        /// <summary>
        /// the id of the workout
        /// </summary>
        [Required]
        public Guid Id { get; set; }
        /// <summary>
        /// the name of the workout
        /// </summary>
        public string? Name { get; set; }
        /// <summary>
        /// the main muscle that the workout targets
        /// </summary>
        public string? MainMuscle { get; set; }
        /// <summary>
        /// the description of the workout
        /// </summary>
        public string? Description { get; set; }
        /// <summary>
        /// link for someone explaining the workout
        /// </summary>
        public string? Link { get; set; }
        /// <summary>
        /// the secondary muscle that the workout targets
        /// </summary>
        public string? SecondaryMuscle { get; set; }
    }
}
