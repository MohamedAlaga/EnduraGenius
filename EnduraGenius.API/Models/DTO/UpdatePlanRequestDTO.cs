namespace EnduraGenius.API.Models.DTO
{
    /// <summary>
    /// DTO to handle the plan update
    /// </summary>
    public class UpdatePlanRequestDTO
    {
        /// <summary>
        /// new name of the plan
        /// </summary>
        public string? PlanName { get; set; }

        /// <summary>
        /// new description of the plan
        /// </summary>
        public string? PlanDescription { get; set; }

        /// <summary>
        /// if the plan is public or private
        /// </summary>
        public bool? IsPublic { get; set; }

        /// <summary>
        /// the new workouts in the plan
        /// </summary>
        public List<CreatePlanWorkoutsDto>? workoutsDtos { get; set; }
    }
}
