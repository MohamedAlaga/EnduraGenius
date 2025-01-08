namespace EnduraGenius.API.Models.Domain
{
    public class Inbody
    {
        public Guid Id { get; set; }
        public string userId { get; set; }
        public string Name { get; set; }
        public int age { get; set; }
        public float weight { get; set; }
        //body mass index
        public float BMI { get; set; }
        //basal metabolic rate
        public float BMR { get; set; }
        //body fat percentage
        public float BFP { get; set; }
        //fat free mass
        public float FFM { get; set; }
        //lean body mass
        public float LBM { get; set; }
        //total body water
        public float TBW { get; set; }
        //caloric needs
        public int CaloricNeed{ get; set; }
        //recommended water intake
        public float WaterIntake { get; set; }
        public float IdealBodyWeight { get; set; }
        public int DailyProtenNeedInGrams { get; set; }

        // navigation property
        public User User { get; set; }
    }
}
