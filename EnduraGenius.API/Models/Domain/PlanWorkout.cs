namespace EnduraGenius.API.Models.Domain
{
    public class PlanWorkout
    {
        public Guid Id { get; set; }
        public Guid PlanId { get; set; }
        public Guid WorkoutId { get; set; }
        public string Reps { get; set; }
        public int Order { get; set; }
        public int DayNumber { get; set; }

        // navigation properties
        public Plan Plan { get; set; }
        public Workout Workout { get; set; }
    }
}
