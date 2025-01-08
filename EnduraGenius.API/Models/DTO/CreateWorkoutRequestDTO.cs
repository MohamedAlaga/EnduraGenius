namespace EnduraGenius.API.Models.DTO
{
    public class CreateWorkoutRequestDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
        public string MainMuscleName { get; set; }
        public string SecondaryMuscleName { get; set; }
    }
}
