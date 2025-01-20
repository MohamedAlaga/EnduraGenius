using System.ComponentModel.DataAnnotations;

namespace EnduraGenius.API.Models.DTO
{
    /// <summary>
    /// DTO to handle the data needed for the creation of the workout
    /// </summary>
    public class CreateWorkoutRequestDTO
    {
        /// <summary>
        /// name of the workout
        /// </summary>
        [Required]
        public string Name { get; set; }
        /// <summary>
        /// description of the workout
        /// </summary>
        [Required]
        public string Description { get; set; }
        /// <summary>
        /// link for someone explaining the workout
        /// </summary>
        [Required]
        public string Link { get; set; }
        /// <summary>
        /// the main muscle that the workout targets
        /// </summary>
        [Required]
        public string MainMuscleName { get; set; }
        /// <summary>
        /// the secondary muscle that the workout targets
        /// </summary>
        public string SecondaryMuscleName { get; set; }
    }
}
