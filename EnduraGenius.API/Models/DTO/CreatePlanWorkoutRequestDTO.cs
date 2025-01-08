using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EnduraGenius.API.Models.DTO
{
    public class CreatePlanWorkoutRequestDTO
    {
        public Guid Plan { get; set; }
        public Guid Workout { get; set; }
        public string Reps { get; set; }
        public int Order { get; set; }
        public int DayNumber { get; set; }
    }
}
