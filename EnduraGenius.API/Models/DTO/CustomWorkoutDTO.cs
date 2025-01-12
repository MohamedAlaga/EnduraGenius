namespace EnduraGenius.API.Models.DTO
{
    public class CustomWorkoutDTO
    {
        public Guid WorkoutId { get; set; }
        public string WorkoutName { get; set; }
        public string Reps { get; set; }
        public int DayNumber { get; set; }
        public int Order { get; set; }
    }
}
