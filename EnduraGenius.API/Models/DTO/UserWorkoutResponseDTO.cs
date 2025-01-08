namespace EnduraGenius.API.Models.DTO
{
    public class UserWorkoutResponseDTO
    {
        public string Name { get; set; }
        public string Descreption { get; set; }
        public float MaxWeight { get; set; }
        public int LastWeight { get; set; }
        public int TimesPerformed { get; set; }
    }
}
