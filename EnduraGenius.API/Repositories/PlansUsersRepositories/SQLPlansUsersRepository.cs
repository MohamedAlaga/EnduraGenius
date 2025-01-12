using EnduraGenius.API.Data;
using EnduraGenius.API.Models.Domain;
using EnduraGenius.API.Repositories.PlanRepositories;
using Microsoft.EntityFrameworkCore;

namespace EnduraGenius.API.Repositories.PlansUsersRepositories
{
    public class SQLPlansUsersRepository : IPlansUsersRepository
    {
        private readonly EnduraGeniusDBContext _dbContext;
        private readonly IPlanRepository _planRepository;
        public SQLPlansUsersRepository(EnduraGeniusDBContext dBContext, IPlanRepository planRepository)
        {
            _dbContext = dBContext;
            _planRepository = planRepository;
        }
        public async Task<PlansUsers?> CreatePlanUser(Plan plan, string userId)
        {

            int maxPlanOrder = (await _dbContext.PlansUsers
                .Where(x => x.UserId == userId)
                .Select(pu => pu.PlanOrder)
                .ToListAsync())
                .DefaultIfEmpty(0)
                .Max();
            Console.WriteLine("max plan order is :" + maxPlanOrder);
            var NewPlanUser = new PlansUsers
            {
                Plan = plan,
                UserId = userId,
                PlanOrder = maxPlanOrder + 1
            };
            await _dbContext.PlansUsers.AddAsync(NewPlanUser);
            await _dbContext.SaveChangesAsync();
            return NewPlanUser;

        }

        public async Task<bool> DeletePlanUser(Guid id)
        {
            var planUser = await _dbContext.PlansUsers.FindAsync(id);
            if (planUser == null)
            {
                return false;
            }
            _dbContext.PlansUsers.Remove(planUser);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<PlansUsers?> GetPlanUserById(Guid id)
        {
            return await _dbContext.PlansUsers.FindAsync(id);
        }

        public async Task<List<PlansUsers>> GetPlansUserByUserId(string userId)
        {
            return await _dbContext.PlansUsers
                .Include(x => x.Plan)
                .Include(x => x.User)
                .Where(x => x.UserId == userId).ToListAsync();
        }

        public async Task<PlansUsers?> GetUserCurrentPlan(string id)
        {
            return await _dbContext.PlansUsers
                .Include(x => x.Plan)
                .Include(x => x.User)
                .Where(x => x.UserId == id)
                .OrderByDescending(x => x.PlanOrder)
                .FirstOrDefaultAsync();
        }

        public async Task<PlansUsers?> SetCurrentPlan(Guid planId, string userId)
        {
            var currentUserPlan = await GetUserCurrentPlan(userId);
            if (currentUserPlan == null)
            {
                return null;
            }
            var maxOrder = currentUserPlan.PlanOrder;
            var newCurrentUserPlan = await _dbContext.PlansUsers.Where(x => x.UserId == userId && x.PlanId == planId)
                .FirstOrDefaultAsync();
            if (newCurrentUserPlan == null)
            {
                return null;
            }
            newCurrentUserPlan.PlanOrder = maxOrder + 1;
            await _dbContext.SaveChangesAsync();
            return newCurrentUserPlan;
        }

        public async Task<bool> UnsubscibeUserFromPlan(string userId, Guid planId)
        {
            var CurrentPlan = await this._planRepository.GetPlanById(planId, userId);
            if (CurrentPlan == null)
            {
                return false;
            }
            if (CurrentPlan.IsPublic == false)
            {
                await this._planRepository.DeletePlan(planId, userId);
                return true;
            }
            var planUser = await _dbContext.PlansUsers
                .Where(x => x.UserId == userId && x.PlanId == planId)
                .ToListAsync();
            if (planUser == null)
            {
                return false;
            }
            foreach (var item in planUser)
            {
                _dbContext.PlansUsers.Remove(item);
            }
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
