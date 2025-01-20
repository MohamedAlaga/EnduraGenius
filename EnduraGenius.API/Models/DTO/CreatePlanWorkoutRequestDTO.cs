using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EnduraGenius.API.Models.DTO
{
    /// <summary>
    /// DTO to handle the data needed for the creation of the plan workout
    /// </summary>
    public class CreatePlanWorkoutRequestDTO
    {
        /// <summary>
        /// the id of the plan
        /// </summary>
        [Required]
        public Guid Plan { get; set; }
        /// <summary>
        /// the id of the workout
        /// </summary>
        [Required]
        public Guid Workout { get; set; }
        /// <summary>
        /// the number of reps 
        /// ex : "12 * 12 * 10 * 8"
        /// </summary>
        [Required]
        public string Reps { get; set; }
        /// <summary>
        /// the order of the workout
        /// </summary>
        [Required]
        public int Order { get; set; }
        /// <summary>
        /// number of the day to do the workout
        /// </summary>
        [Required]
        public int DayNumber { get; set; }
    }
}
