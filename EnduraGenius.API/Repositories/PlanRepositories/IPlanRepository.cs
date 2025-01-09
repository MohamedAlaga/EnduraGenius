using EnduraGenius.API.Models.Domain;

namespace EnduraGenius.API.Repositories.PlanRepositories
{
    public interface IPlanRepository
    {
        Task<Plan?> CreatePlan(string name, string description, string userId, bool isPublic);
        Task<bool> DeletePlan(Guid id);
        Task<List<Plan>> GetPlansByCreatorId(string userId);
        Task<Plan?> UpdatePlan(Guid id,string userId, string? name, string? description, bool? isPublic);
        Task<Plan?> GetPlanById(Guid id,string userId);
        Task<List<Plan>> GetPublicPlans(string userId);
    }
}
