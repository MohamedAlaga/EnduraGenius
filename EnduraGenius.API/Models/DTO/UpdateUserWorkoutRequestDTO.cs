namespace EnduraGenius.API.Models.DTO
{
    public class UpdateUserWorkoutRequestDTO
    {
        public float? MaxWeight { get; set; }
        public float? LastWeight { get; set; }
        public int? TimesPerformed { get; set; }
    }
}
