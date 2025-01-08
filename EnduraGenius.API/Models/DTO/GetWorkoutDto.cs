namespace EnduraGenius.API.Models.DTO
{
    public class GetWorkoutDto
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? MainMuscle { get; set; }
        public string? Description { get; set; }
        public string? Link { get; set; }
        public string? SecondaryMuscle { get; set; }
    }
}
