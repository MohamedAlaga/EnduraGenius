using EnduraGenius.API.Models.Domain;

namespace EnduraGenius.API.Repositories.InbodyRepository
{
    /// <summary>
    /// Interface for the Inbody Repository
    /// claculate the inbody data
    /// </summary>
    public interface IInbodyRepository
    {
        /// <summary>
        /// Calculate the body mass index
        /// </summary>
        /// <param name="weight">user weight in kg</param>
        /// <param name="height">user height in cm</param>
        /// <returns>
        /// float: the basal metabolic rate
        /// </returns>
        float CalculateBMI(float weight, float height);

        /// <summary>
        /// Calculate the basal metabolic rate
        /// </summary>
        /// <param name="weight">user weight in KG</param>
        /// <param name="height">user height in cm</param>
        /// <param name="age">user age in years</param>
        /// <param name="isMale">is the user male</param>
        /// <returns>
        /// float: the basal metabolic rate
        /// </returns>
        float CalculateBMR(float weight, float height, int age,bool isMale);

        /// <summary>
        /// Calculate the body fat percentage
        /// </summary>
        /// <param name="BMI">user body mass index</param>
        /// <param name="age">user age in years</param>
        /// <param name="isMale">is the user male</param>
        /// <returns>
        /// float : the body fat percentage
        /// </returns>
        float CalculateBodyFatPercentage(float BMI, int age, bool isMale);

        /// <summary>
        /// Calculate the free fat mass
        /// </summary>
        /// <param name="weight">user weight in KG</param>
        /// <param name="bodyFatPercentage">body fat percentage</param>
        /// <returns>
        /// flaot : the free fat mass in kg
        /// </returns>
        float CalculateFreeFatMass(float weight, float bodyFatPercentage);

        /// <summary>
        /// Calculate the total body water
        /// </summary>
        /// <param name="FreeFatMass">the user weight with no fats</param>
        /// <returns>
        /// float : the total body water in kg
        /// </returns>
        float CalculateTotalBodyWater(float FreeFatMass);

        /// <summary>
        /// Calculate the lean body mass
        /// </summary>
        /// <param name="FreeFatMass">the user weight with no fats</param>
        /// <param name="TotalBodyWater">total weight of body water</param>
        /// <returns>
        /// float : the lean body mass
        /// </returns>
        float CalculateLeanBodyMass(float FreeFatMass, float TotalBodyWater);

        /// <summary>
        /// Calculate the caloric needs
        /// </summary>
        /// <param name="BMR">user basal metabolic rate</param>
        /// <param name="activityLevel">user activity level</param>
        /// <returns></returns>
        float CalculateCaloricNeeds(float BMR, int activityLevel);

        /// <summary>
        /// Calculate the recommended water intake
        /// </summary>
        /// <param name="weight">user body weight in kg</param>
        /// <returns>
        /// floatt : the recommended water intake in liters
        /// </returns>
        float CalculateRecommendedWaterIntake(float weight);

        /// <summary>
        /// Calculate the ideal body weight
        /// </summary>
        /// <param name="height">user height in cm</param>
        /// <param name="isMale">is the user male or not</param>
        /// <returns></returns>
        float CalculateIdealBodyWeight(float height, bool isMale);

        /// <summary>
        /// Calculate the daily protein needs
        /// </summary>
        /// <param name="weight"></param>
        /// <param name="activityLevel"></param>
        /// <returns></returns>
        float CalculateDailyProteinNeeds(float weight,int activityLevel);

        /// <summary>
        /// Get all user previous inbodies data
        /// </summary>
        /// <param name="id">user id</param>
        /// <returns>
        /// list of the user inbodies data
        /// </returns>
        Task<List<Inbody>> GetInbodyByUserId(string id);

        /// <summary>
        /// Get the inbody data by id
        /// </summary>
        /// <param name="id">inbody id</param>
        /// <param name="userId">user id</param>
        /// <returns></returns>
        Task<Inbody?> GetInbodyAsync(Guid id, string userId);

        /// <summary>
        /// create new inbody test
        /// </summary>
        /// <param name="userId">user id</param>
        /// <param name="ActivityLevel">user activity level ,range from 0 to 4</param>
        /// <param name="name">the name of the new inbody</param>
        /// <returns>
        /// new inbody test result
        /// </returns>
        Task<Inbody?> InsertInbodyAsync(string userId, int ActivityLevel, string name);

        /// <summary>
        /// delete inbody test
        /// </summary>
        /// <param name="ID">inbody test id</param>
        /// <param name="userId"> user id</param>
        /// <returns>
        /// true if the inbody test is deleted successfully
        /// false if any of the data is not valid
        /// </returns>
        Task<bool> DeleteInbody(Guid ID, string userId);

    }
}
