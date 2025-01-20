namespace EnduraGenius.API.Models.DTO
{
    /// <summary>
    /// DTO to handle the user workout update
    /// </summary>
    public class UpdateUserWorkoutRequestDTO
    {
        /// <summary>
        /// the new user max weight
        /// </summary>
        public float? MaxWeight { get; set; }
        /// <summary>
        /// the last weight the user lifted
        /// </summary>
        public float? LastWeight { get; set; }
        /// <summary>
        /// how many times the user performed the workout
        /// </summary>
        public int? TimesPerformed { get; set; }
    }
}
