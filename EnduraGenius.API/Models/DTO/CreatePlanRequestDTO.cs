using System.ComponentModel.DataAnnotations;

namespace EnduraGenius.API.Models.DTO
{
    public class CreatePlanRequestDTO
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Descreption { get; set; }
        [Required]
        public List<CreatePlanWorkoutsDto> workoutsDtos { get; set; }

    }
}
