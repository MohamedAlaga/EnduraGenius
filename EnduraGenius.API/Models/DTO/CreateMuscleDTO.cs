using System.ComponentModel.DataAnnotations;

namespace EnduraGenius.API.Models.DTO
{
    /// <summary>
    /// DTO to handle muscle creation
    /// </summary>
    public class CreateMuscleDTO
    {
        /// <summary>
        /// name of the muscle
        /// </summary>
        [Required]
        public string Name { get; set; }
        /// <summary>
        /// descreption for the muscle postion and movment
        /// </summary>
        [Required]
        public string Description { get; set; }
    }
}
