using EnduraGenius.API.Models.Domain;
using EnduraGenius.API.Models.DTO;

namespace EnduraGenius.API.Repositories.PlanRepositories
{
    /// <summary>
    /// Plan Repository interface
    /// </summary>
    public interface IPlanRepository
    {
        /// <summary>
        /// create new plan
        /// </summary>
        /// <param name="name">plan name</param>
        /// <param name="description">plan descreption</param>
        /// <param name="userId">plan creator id </param>
        /// <param name="isPublic">is the plan public or private</param>
        /// <returns>
        /// plan object if created successfully
        /// </returns>
        Task<Plan?> CreatePlan(string name, string description, string userId, bool isPublic);

        /// <summary>
        /// delete plan
        /// </summary>
        /// <param name="PlanId">plan id</param>
        /// <param name="UserId">creator id</param>
        /// <returns>
        /// true if the plan deleted successfully
        /// </returns>
        Task<bool> DeletePlan(Guid PlanId,string UserId);

        /// <summary>
        /// get all plans created by the user
        /// </summary>
        /// <param name="userId">the creator id</param>
        /// <returns>
        /// list of plans created by the user
        /// </returns>
        Task<List<Plan>> GetPlansByCreatorId(string userId);

        /// <summary>
        /// update plan
        /// </summary>
        /// <param name="id">plan id</param>
        /// <param name="userId">creator id</param>
        /// <param name="name">plan name</param>
        /// <param name="description">plan descrption</param>
        /// <param name="isPublic">true if the plan public</param>
        /// <param name="workoutsDtos">DTO contians new plan data</param>
        /// <returns>
        /// new plan object if updated successfully
        /// </returns>
        Task<Plan?> UpdatePlan(Guid id,string userId, string? name, string? description, bool? isPublic, List<CreatePlanWorkoutsDto>? workoutsDtos);

        /// <summary>
        /// get plan by id
        /// </summary>
        /// <param name="id">id of the plan</param>
        /// <param name="userId">creator id</param>
        /// <returns></returns>
        Task<Plan?> GetPlanById(Guid id,string userId);

        /// <summary>
        /// get all public plans
        /// </summary>
        /// <param name="userId">user id</param>
        /// <returns>
        /// list of the public plans
        /// </returns>
        Task<List<Plan>> GetPublicPlans(string userId);

        /// <summary>
        /// create new custom pro split workout plan based on training history
        /// </summary>
        /// <param name="userId">the user id</param>
        /// <returns>
        /// list of the custom workout plan DTO
        /// </returns>
        Task<List<CustomWorkoutDTO>> CreateProSplitWokoutPlan(string userId);

        /// <summary>
        /// create new custom Upper lower workout plan based on training history
        /// </summary>
        /// <param name="userId">the user id</param>
        /// <returns>
        /// list of the custom workout plan DTO
        /// </returns>
        Task<List<CustomWorkoutDTO>> CreateUpperLowerPlan(string userId);
    }
}
