using System.ComponentModel.DataAnnotations;

namespace EnduraGenius.API.Models.DTO
{
    /// <summary>
    /// DTO to handle the data needed for planworkout object creation
    /// </summary>
    public class CreatePlanWorkoutsDto
    {
        /// <summary>
        /// workout id
        /// </summary>
        [Required]
        public Guid WorkoutId { get; set; }
        /// <summary>
        /// reps to be done
        /// ex : "12 * 12 * 10 * 8"
        /// </summary>
        [Required]
        public string Reps { get; set; }
        /// <summary>
        /// the order of the workout in the day
        /// </summary>
        [Required]
        public int Order { get; set; }
        /// <summary>
        ///  the number of the day the Workout will be done
        /// </summary>
        [Required]
        public int DayNumber { get; set; }

    }
}
