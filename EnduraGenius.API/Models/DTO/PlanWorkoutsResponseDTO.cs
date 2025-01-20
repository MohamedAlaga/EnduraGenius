using System.ComponentModel.DataAnnotations;

namespace EnduraGenius.API.Models.DTO
{
    /// <summary>
    /// DTO to handle the data needed to return the plan workouts
    /// </summary>
    public class PlanWorkoutsResponseDTO
    {
        /// <summary>
        /// the planworkout record id
        /// </summary>
        [Required]
        public Guid PlanWorkoutID { get; set; }
        /// <summary>
        /// the workout id
        /// </summary>
        [Required]
        public Guid WorkoutID { get; set; }
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
        /// <summary>
        /// the number of sets for the workout
        /// ex : "12 * 12 * 10 * 8"
        /// </summary>
        public string? Reps { get; set; }
        /// <summary>
        /// the order of the workout in the day
        /// </summary>
        public int? Order { get; set; }
        /// <summary>
        /// the number of the day the Workout will be done
        /// </summary>
        public int? DayNumber { get; set; }
    }
}
