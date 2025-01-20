namespace EnduraGenius.API.Models.DTO
{
    /// <summary>
    /// DTO to handle the user update
    /// </summary>
    public class UpdateUserBodyDTO
    {
        /// <summary>
        /// weight of the user
        /// </summary>
        public float? Weight { get; set; }
        /// <summary>
        /// tall of the user
        /// </summary>
        public int? Tall { get; set; }
        /// <summary>
        /// age of the user
        /// </summary>
        public int? Age { get; set; }
        /// <summary>
        /// set true if the user male and false if the user female
        /// </summary>
        public bool? IsMale { get; set; }
        /// <summary>
        /// set true if the user profile is public and false if the user profile is private
        /// </summary>
        public bool? IsPublic { get;set; }
    }
}
