using EnduraGenius.API.Models.Domain;

namespace EnduraGenius.API.Repositories.InbodyRepository
{
    public interface IInbodyRepository
    {
        float CalculateBMI(float weight, float height);
        float CalculateBMR(float weight, float height, int age,bool isMale);
        float CalculateBodyFatPercentage(float BMI, int age, bool isMale);
        float CalculateFreeFatMass(float weight, float bodyFatPercentage);
        float CalculateTotalBodyWater(float FreeFatMass);
        float CalculateLeanBodyMass(float FreeFatMass, float TotalBodyWater);
        float CalculateCaloricNeeds(float BMR, int activityLevel);
        float CalculateRecommendedWaterIntake(float weight);
        float CalculateIdealBodyWeight(float height, bool isMale);
        float CalculateDailyProteinNeeds(float weight,int activityLevel);
        Task<List<Inbody>> GetInbodyByUserId(string id);
        Task<Inbody?> GetInbodyAsync(Guid id, string userId);
        Task<Inbody?> InsertInbodyAsync(string userId, int ActivityLevel, string name);
        Task<bool> DeleteInbody(Guid ID, string userId);

    }
}
