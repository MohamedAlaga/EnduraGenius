namespace EnduraGenius.API.Models.DTO
{
    public class PlanResponseDTO
    {
        public Guid Id { get; set; }
        public string name { get; set; }
        public string Descreption { get; set; }
        public bool IsPublic { get; set; }
        public string PlanCreatedBy { get; set; }
        public List<PlanWorkoutsResponseDTO> workouts { get; set; }
    }
}
