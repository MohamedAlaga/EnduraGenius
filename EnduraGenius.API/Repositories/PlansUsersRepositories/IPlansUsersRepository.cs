using EnduraGenius.API.Models.Domain;

namespace EnduraGenius.API.Repositories.PlansUsersRepositories
{
    public interface IPlansUsersRepository
    {
        /// <summary>
        /// Create a new PlanUser object
        /// </summary>
        /// <param name="plan">subscibed plan</param>
        /// <param name="userId">user id</param>
        /// <returns>
        /// new PlanUser object if created successfully
        /// </returns>
        Task<PlansUsers?> CreatePlanUser(Plan plan, string userId);

        /// <summary>
        /// Delete a PlanUser object
        /// </summary>
        /// <param name="id">planUser object id</param>
        /// <returns>
        /// true if deleted successfully otherwise false
        /// </returns>
        Task<bool> DeletePlanUser(Guid id);

        /// <summary>
        /// Get all the plans that a user is subscribed to
        /// </summary>
        /// <param name="id">the user id</param>
        /// <returns>
        /// list of planUsers that the user is subscribed to
        /// </returns>
        Task<List<PlansUsers>> GetPlansUserByUserId(string id);

        /// <summary>
        /// Get a planUser by its id
        /// </summary>
        /// <param name="id">planuser object id</param>
        /// <returns>
        /// requested planUser object if found otherwise null
        /// </returns>
        Task<PlansUsers?> GetPlanUserById(Guid id);

        /// <summary>
        /// get the current plan that user is following
        /// </summary>
        /// <param name="id">the user id</param>
        /// <returns>
        /// plan user object if found otherwise null
        /// </returns>
        Task<PlansUsers?> GetUserCurrentPlan(string id);

        /// <summary>
        /// Set a plan as the current plan for a user
        /// </summary>
        /// <param name="planId">plan id</param>
        /// <param name="userId">user id</param>
        /// <returns>
        /// the planUser object if set successfully otherwise null
        /// </returns>
        Task<PlansUsers?> SetCurrentPlan(Guid planId, string userId);

        /// <summary>
        /// Unsubscribe a user from a plan
        /// </summary>
        /// <param name="userId">user id</param>
        /// <param name="planId">plan id</param>
        /// <returns>
        /// true if unsubscribed successfully otherwise false
        /// </returns>
        Task<bool> UnsubscibeUserFromPlan(string userId, Guid planId);
    }
}
