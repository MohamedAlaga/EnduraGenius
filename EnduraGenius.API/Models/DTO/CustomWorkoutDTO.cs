using System.ComponentModel.DataAnnotations;

namespace EnduraGenius.API.Models.DTO
{
    /// <summary>
    /// DTO to handle the response of the custom workout
    /// </summary>
    public class CustomWorkoutDTO
    {
        /// <summary>
        /// the id of the workout
        /// </summary>
        [Required]
        public Guid WorkoutId { get; set; }
        /// <summary>
        /// the name of the workout
        /// </summary>
        [Required]
        public string WorkoutName { get; set; }
        /// <summary>
        /// the description of the workout
        /// ex : "12 * 12 * 10 * 8"
        /// </summary>
        [Required]
        public string Reps { get; set; }
        /// <summary>
        /// the number of the day the Workout will be done
        /// </summary>
        [Required]
        public int DayNumber { get; set; }
        /// <summary>
        /// the order of the workout in the day
        /// </summary>
        [Required]
        public int Order { get; set; }
    }
}
