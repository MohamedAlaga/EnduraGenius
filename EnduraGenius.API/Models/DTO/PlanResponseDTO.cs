namespace EnduraGenius.API.Models.DTO
{
    /// <summary>
    /// DTO to handle the plans response
    /// </summary>
    public class PlanResponseDTO
    {
        /// <summary>
        /// the id of the plan
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// the name of the plan
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// the description of the plan
        /// </summary>
        public string Descreption { get; set; }
        /// <summary>
        /// if the plan is public or private
        /// </summary>
        public bool IsPublic { get; set; }
        /// <summary>
        /// the user who created the plan
        /// </summary>
        public string PlanCreatedBy { get; set; }
        /// <summary>
        /// the workouts in the plan
        /// </summary>
        public List<PlanWorkoutsResponseDTO> workouts { get; set; }
    }
}
