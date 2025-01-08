namespace EnduraGenius.API.Models.DTO
{
    public class InbodyResponseDTO
    {
        public Guid Id { get; set; }
        public string userName   { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public float Weight { get; set; }
        public float bodyMassIndex { get; set; }
        public float basalMetabolicRate { get; set; }
        public float bodyFatPercentage { get; set; }
        public float fatFreeMass { get; set; }
        public float leanBodyMass { get; set; }
        public float totalBodyWater { get; set; }
        public int CaloricNeed { get; set; }
        public float WaterIntake { get; set; }
        public float IdealBodyWeight { get; set; }
        public int DailyProtenNeedInGrams { get; set; }
    }
}
