namespace EnduraGenius.API.Models.Domain
{
    public class Workout
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid? MainMuscleId { get; set; }
        public Guid? SecondaryMuscleId { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
        public string? WorkoutCreatedBy { get; set; }
        public bool IsCertified { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        // navigation properties
        public Muscle? MainMuscle { get; set; }
        public Muscle? SecondaryMuscle { get; set; }
        public User? WorkoutCreator { get; set; }
    }
}
