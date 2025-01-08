using EnduraGenius.API.Data;
using EnduraGenius.API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace EnduraGenius.API.Repositories.InbodyRepository
{
    public class SQLInbodyRepository : IInbodyRepository
    {
        private readonly EnduraGeniusDBContext _context;
        public SQLInbodyRepository(EnduraGeniusDBContext dBContext)
        {
            this._context = dBContext; 
        }
        public float CalculateBMI(float weight, float height)
        {
            height = height / 100;
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

            return weight * (1 - (bodyFatPercentage/100));
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

        public async Task<User?> GetUserById(string userId)
        {
            if (userId == null)
            {
                return null;
            }
            await _context.Users.FindAsync(userId);
            return await _context.Users.FindAsync(userId);
        }

        public async Task<List<Inbody>> GetInbodyByUserId(string id)
        {
            if (id == null)
            {
                return [];
            }
            return await _context.Inbodies.Where(x => x.userId ==id).Include(x => x.User).ToListAsync();
        }

        public async Task<Inbody?> GetInbodyAsync(Guid id,string userId)
        {
            return await _context.Inbodies.Where(x => x.Id == id && x.userId == userId).Include(x => x.User).FirstOrDefaultAsync();
        }

        public async Task<Inbody?> InsertInbodyAsync(string userId, int ActivityLevel,string name)
        {
            var user = await this.GetUserById(userId);
            if (user == null)
            {
                return null;
            }
            float weight = user.WeightInKg;
            float height = user.TallInCm;
            int age = user.Age;
            bool isMale = user.IsMale;
            var inbody = new Inbody();
            inbody.userId = userId;
            inbody.Name = name;
            inbody.age = age;
            inbody.weight = weight;
            inbody.BMI = this.CalculateBMI(weight, height);
            inbody.BMR = this.CalculateBMR(weight, height, age, isMale);
            inbody.BFP = this.CalculateBodyFatPercentage(inbody.BMI, age, isMale);
            inbody.FFM = this.CalculateFreeFatMass(weight, inbody.BFP);
            inbody.LBM = this.CalculateLeanBodyMass(inbody.FFM, inbody.TBW);
            inbody.TBW = this.CalculateTotalBodyWater(inbody.FFM);
            inbody.CaloricNeed = (int)(this.CalculateCaloricNeeds(inbody.BMR, ActivityLevel));
            inbody.WaterIntake = this.CalculateRecommendedWaterIntake(weight);
            inbody.IdealBodyWeight = this.CalculateIdealBodyWeight(height, isMale);
            inbody.DailyProtenNeedInGrams = (int)this.CalculateDailyProteinNeeds(weight, ActivityLevel);
            _context.Inbodies.Add(inbody);
            await _context.SaveChangesAsync();
            return await this.GetInbodyAsync(inbody.Id,userId);
        }

        public async Task<bool> DeleteInbody(Guid ID, string userId)
        {
            var inbody = await _context.Inbodies.Where(x => x.Id == ID && x.userId == userId).FirstOrDefaultAsync();
            if (inbody == null)
            {
                return false;
            }
            _context.Inbodies.Remove(inbody);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
