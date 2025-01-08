using EnduraGenius.API.Models.Domain;

namespace EnduraGenius.API.Repositories.PlansUsersRepositories
{
    public interface IPlansUsersRepository
    {
        Task<PlansUsers?> CreatePlanUser(Plan plan, string userId);
        Task<bool> DeletePlanUser(Guid id);
        Task<List<PlansUsers>> GetPlansUserByUserId(string id);
        Task<PlansUsers?> GetPlanUserById(Guid id);
        Task<PlansUsers?> GetUserCurrentPlan(string id);
        Task<PlansUsers?> SetCurrentPlan(Guid planId, string userId);
    }
}
