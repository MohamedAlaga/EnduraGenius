using EnduraGenius.API.Models.Domain;
using EnduraGenius.API.Models.DTO;

namespace EnduraGenius.API.Repositories.PlanRepositories
{
    public interface IPlanRepository
    {
        Task<Plan?> CreatePlan(string name, string description, string userId, bool isPublic);
        Task<bool> DeletePlan(Guid PlanId,string UserId);
        Task<List<Plan>> GetPlansByCreatorId(string userId);
        Task<Plan?> UpdatePlan(Guid id,string userId, string? name, string? description, bool? isPublic, List<CreatePlanWorkoutsDto>? workoutsDtos);
        Task<Plan?> GetPlanById(Guid id,string userId);
        Task<List<Plan>> GetPublicPlans(string userId);
        Task<List<CustomWorkoutDTO>> CreateProSplitWokoutPlan(string userId);
        Task<List<CustomWorkoutDTO>> CreateUpperLowerPlan(string userId);
    }
}
