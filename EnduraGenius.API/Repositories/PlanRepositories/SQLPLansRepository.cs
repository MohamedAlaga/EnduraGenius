using EnduraGenius.API.Data;
using EnduraGenius.API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace EnduraGenius.API.Repositories.PlanRepositories
{
    public class SQLPLansRepository : IPlanRepository
    {
        private readonly EnduraGeniusDBContext _context;
        public SQLPLansRepository(EnduraGeniusDBContext dBContext)
        {
            _context = dBContext;
        }

        public async Task<Plan?> CreatePlan(string name, string description, string userId, bool isPublic)
        {
            try
            {
                var plan = new Plan
                {
                    Name = name,
                    Descreption = description,
                    PlanCreatedBy = userId,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    IsPublic = isPublic
                };
                await _context.Plans.AddAsync(plan);
                await _context.SaveChangesAsync();
                var newPlan = await _context.Plans.Include(x => x.planCreator).FirstOrDefaultAsync(x => x.Id == plan.Id);
                return newPlan;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<bool> DeletePlan(Guid id)
        {
            var plan = await _context.Plans.FindAsync(id);
            if (plan == null)
            {
                return false;
            }
            _context.Plans.Remove(plan);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Plan?> GetPlanById(Guid id)
        {
            return await _context.Plans.Include(x => x.planCreator).Where(x=> x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<Plan>> GetPlansByCreatorId(string userId)
        {
            return await _context.Plans.Include(x => x.planCreator).Where(x => x.PlanCreatedBy == userId).ToListAsync();
        }

        public async Task<List<Plan>> GetPublicPlans()
        {
            return await _context.Plans.Include(x => x.planCreator).Where(x => x.IsPublic == false).ToListAsync();
        }

        public async Task<Plan?> UpdatePlan(Guid id, string? name, string? description, bool? isPublic)
        {
            var plan = _context.Plans.Find(id);
            if (plan == null)
            {
                return await Task.FromResult<Plan?>(null);
            }
            plan.Name = name ?? plan.Name;
            plan.Descreption = description ?? plan.Descreption;
            plan.IsPublic = isPublic ?? plan.IsPublic;
            plan.UpdatedAt = DateTime.Now;
            await _context.SaveChangesAsync();
            return await Task.FromResult<Plan?>(plan);
        }
    }
}
