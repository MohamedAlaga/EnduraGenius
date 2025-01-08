namespace EnduraGenius.API.Models.Domain
{
    public class UserWorkout
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }

        public Guid WorkoutId { get; set; }
        public int TimesPerformed { get; set; } = 0;
        public float MaxWeight { get; set; } = 0;
        public float LastWeight { get; set; } = 0;

        // navigation properties
        public User User { get; set; }
        public Workout Workout { get; set; }
    }
}
