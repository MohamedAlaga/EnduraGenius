namespace EnduraGenius.API.Models.DTO
{
    public class UpdatePlanWorkoutRequestDTO
    {
        public Guid? NewWorkoutId { get; set; }
        public string? NewReps { get; set; }
        public int? NewOrder { get; set; }
        public int? NewDayNumber { get; set; }
    }
}
