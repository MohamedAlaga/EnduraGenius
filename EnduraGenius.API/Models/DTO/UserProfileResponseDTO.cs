namespace EnduraGenius.API.Models.DTO
{
    public class UserProfileResponseDTO
    {
        public string UserName { get; set; }
        public string Email { get; set; }

        public int Age { get; set; }
        public float WeightInKg { get; set; }
        public int TallInCm { get; set; }
        public bool IsMale { get; set; }
        public int Points { get; set; }
        public string? ProfilePicture { get; set; }
        public List<UserWorkoutResponseDTO> userWorkouts { get; set; }
    }
}
