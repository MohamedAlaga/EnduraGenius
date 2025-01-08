namespace EnduraGenius.API.Repositories.InbodyRepository
{
    public class SQLInbodyRepository : IInbodyRepository
    {
        public float CalculateBMI(float weight, float height)
        {
            return weight / (height * height);
        }

        public float CalculateBMR(float weight, float height, int age, bool isMale)
        {
            int genderModifier = isMale ? 5 : -161;
            return (10*weight) + (6.25f * height) - (5 * age) + genderModifier;
        }

        public float CalculateBodyFatPercentage(float BMI, int age, bool isMale)
        {
            double genderModifier = isMale ? 16.2 : 5.4;
            return (float)((1.2 * BMI) + (0.23 * age) - genderModifier);
        }

        public float CalculateCaloricNeeds(float BMR, int activityLevel)
        {
            if (activityLevel > 4)
            {
                return 0;
            }
            double[] activityMultipliers = [1.2,1.375,1.55,1.725,1.9];
            return (float)(BMR * activityMultipliers[activityLevel]);
        }

        public float CalculateDailyProteinNeeds(float weight, int activityLevel)
        {
            double[] activityMultipliers = [0.8, 1.2, 1.4, 1.6, 1.8];
            return (float)(weight * activityMultipliers[activityLevel]);
        }

        public float CalculateFreeFatMass(float weight, float bodyFatPercentage)
        {
            return weight * (1 - bodyFatPercentage);
        }

        public float CalculateIdealBodyWeight(float height, bool isMale)
        {
            float heightInInches = (float)(height/2.54);
            float genderModifier = (float)(isMale ? 50 : 45.5);
            return (float)(genderModifier + (2.3 * (heightInInches - 60)));
        }

        public float CalculateLeanBodyMass(float FreeFatMass, float TotalBodyWater)
        {
            return FreeFatMass + TotalBodyWater;
        }

        public float CalculateRecommendedWaterIntake(float weight)
        {
            return weight * 0.033f;
        }

        public float CalculateTotalBodyWater(float FreeFatMass)
        {
            return FreeFatMass * 0.73f;
        }
    }
}
