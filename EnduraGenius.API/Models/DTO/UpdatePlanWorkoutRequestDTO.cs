namespace EnduraGenius.API.Models.DTO
{
    /// <summary>
    /// DTO to handle the update plan workout request
    /// </summary>
    public class UpdatePlanWorkoutRequestDTO
    {
        /// <summary>
        /// the id of the new workout
        /// </summary>
        public Guid? NewWorkoutId { get; set; }

        /// <summary>
        /// the new reps
        /// ex : " 12 * 10 * 10 * 8"
        /// </summary>
        public string? NewReps { get; set; }

        /// <summary>
        /// the new order of the workout    
        /// </summary>
        public int? NewOrder { get; set; }

        /// <summary>
        /// the new day number
        /// </summary>
        public int? NewDayNumber { get; set; }
    }
}
