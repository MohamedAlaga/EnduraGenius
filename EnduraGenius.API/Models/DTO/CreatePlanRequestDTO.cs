using System.ComponentModel.DataAnnotations;

namespace EnduraGenius.API.Models.DTO
{
    public class CreatePlanRequestDTO
    {
        /// <summary>
        /// name of the plan
        /// </summary>
        [Required]
        public string Name { get; set; }
        /// <summary>
        /// descreption and notes for the plan and how to use it
        /// </summary>
        [Required]
        public string Descreption { get; set; }
        /// <summary>
        /// all the workouts included in the plan
        /// </summary>
        [Required]
        public List<CreatePlanWorkoutsDto> workoutsDtos { get; set; }

    }
}
