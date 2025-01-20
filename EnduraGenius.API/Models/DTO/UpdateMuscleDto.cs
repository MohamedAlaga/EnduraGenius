namespace EnduraGenius.API.Models.DTO
{
    /// <summary>
    /// DTO to handle the muscle update
    /// </summary>
    public class UpdateMuscleDto
    {
        /// <summary>
        /// name of the muscle
        /// </summary>
        public string? Name { get; set; }
        /// <summary>
        /// descreption for the muscle postion and movment
        /// </summary>
        public string? Description { get; set; }
    }
}
