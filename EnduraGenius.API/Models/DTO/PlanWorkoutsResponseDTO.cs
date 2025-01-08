namespace EnduraGenius.API.Models.DTO
{
    public class PlanWorkoutsResponseDTO
    {

        public Guid PlanWorkoutID { get; set; }
        public Guid WorkoutID { get; set; }
        public string? Name { get; set; }
        public string? MainMuscle { get; set; }
        public string? Description { get; set; }
        public string? Link { get; set; }
        public string? SecondaryMuscle { get; set; }
        public string? Reps { get; set; }
        public int? Order { get; set; }
        public int? DayNumber { get; set; }
    }
}
