namespace EnduraGenius.API.Models.DTO
{
    public class CreatePlanWorkoutsDto
    {
        public Guid WorkoutId { get; set; }
        public string Reps { get; set; }
        public int Order { get; set; }
        public int DayNumber { get; set; }

    }
}
