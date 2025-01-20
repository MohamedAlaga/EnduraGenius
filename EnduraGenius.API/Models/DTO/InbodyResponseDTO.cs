using System.ComponentModel.DataAnnotations;

namespace EnduraGenius.API.Models.DTO
{
    /// <summary>
    /// DTO to handle the data sent to the client after the inbody test
    /// </summary>
    public class InbodyResponseDTO
    {
        /// <summary>
        /// the id of the new inbody test
        /// </summary>
        [Required]
        public Guid Id { get; set; }
        /// <summary>
        /// the user name of the person who did the inbody test
        /// </summary>
        [Required]
        public string userName   { get; set; }
        /// <summary>
        /// the name of the inbody test
        /// </summary>
        [Required]
        public string Name { get; set; }
        /// <summary>
        /// the age of the person who did the inbody test
        /// </summary>
        [Required]
        public int Age { get; set; }
        /// <summary>
        /// the weight of the person who did the inbody test
        /// </summary>
        [Required]
        public float Weight { get; set; }
        /// <summary>
        /// the body Mass Index (BMI) of the person who did the inbody test
        /// </summary>
        [Required]
        public float bodyMassIndex { get; set; }
        /// <summary>
        /// the basal Metabolic Rate (BMR) of the person who did the inbody test
        /// </summary>
        [Required]
        public float basalMetabolicRate { get; set; }
        /// <summary>
        /// the body fat percentage of the person who did the inbody test
        /// </summary>
        [Required]
        public float bodyFatPercentage { get; set; }
        /// <summary>
        /// the body mass without fats of the person who did the inbody test
        /// </summary>
        [Required]
        public float fatFreeMass { get; set; }
        /// <summary>
        /// the lean body mass of the person who did the inbody test
        /// </summary>
        [Required]
        public float leanBodyMass { get; set; }
        /// <summary>
        /// the total body water of the person who did the inbody test
        /// </summary>
        [Required]
        public float totalBodyWater { get; set; }
        /// <summary>
        /// the need of calories for the person who did the inbody test
        /// </summary>
        [Required]
        public int CaloricNeed { get; set; }
        /// <summary>
        /// the suggested water intake for the person who did the inbody test
        /// </summary>
        [Required]
        public float WaterIntake { get; set; }
        /// <summary>
        /// the ideal body weight for the person who did the inbody test
        /// </summary>
        [Required]
        public float IdealBodyWeight { get; set; }
        /// <summary>
        /// the daily protein need in grams for the person who did the inbody test
        /// </summary>
        [Required]
        public int DailyProtenNeedInGrams { get; set; }
    }
}
