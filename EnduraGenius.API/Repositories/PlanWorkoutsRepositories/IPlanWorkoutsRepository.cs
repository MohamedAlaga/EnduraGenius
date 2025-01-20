using EnduraGenius.API.Models.Domain;

namespace EnduraGenius.API.Repositories.PlanWorkoutsRepositories
{
    /// <summary>
    /// Interface for the PlanWorkoutsRepository
    /// </summary>
    public interface IPlanWorkoutsRepository
    {
        /// <summary>
        /// Create a PlanWorkout object
        /// </summary>
        /// <param name="plan">plan object</param>
        /// <param name="Workout"> workout object</param>
        /// <param name="Reps">the number of the reps that the user will perform , ex : "12 * 12 * 10 * 8"</param>
        /// <param name="dayNumber">the day when the user will perform this workout</param>
        /// <param name="order">the order of the workout in the day</param>
        /// <returns>
        /// if the creation was successful, the PlanWorkout object will be returned, otherwise null
        /// </returns>
        Task<PlanWorkout?> CreatePlanWorkout(Plan plan, Workout Workout, string Reps, int dayNumber, int order);

        /// <summary>
        /// Delete a PlanWorkout object
        /// </summary>
        /// <param name="id">the id of the planworkout object</param>
        /// <param name="userId">creator id</param>
        /// <returns>
        /// true if the deletion was successful, otherwise false
        /// </returns>
        Task<bool> DeletePlanWorkout(Guid id, string userId);

        /// <summary>
        /// Get all the PlanWorkout objects that are related to a plan
        /// </summary>
        /// <param name="id">the id of the plan</param>
        /// <param name="userId">the creator id</param>
        /// <returns>
        /// list of PlanWorkout objects
        /// </returns>
        Task<List<PlanWorkout>> GetPlanWorkoutByPlanId(Guid id, string userId);

        /// <summary>
        /// Update a PlanWorkout object
        /// </summary>
        /// <param name="id">the id of plan workout object</param>
        /// <param name="userId">creator user id</param>
        /// <param name="NewWorkoutId">the replaced workout id</param>
        /// <param name="Reps">new reps</param>
        /// <param name="dayNumber">new day number</param>
        /// <param name="Order">the order of the workout in the day</param>
        /// <returns>
        /// a PlanWorkout object if the update was successful, otherwise null
        /// </returns>
        Task<PlanWorkout?> UpdatePlanWorkout(Guid id, string userId, Guid? NewWorkoutId, string? Reps, int? dayNumber, int? Order);
        /// <summary>
        /// Get a PlanWorkout object by its id
        /// </summary>
        /// <param name="id">the planworkout object id</param>
        /// <param name="userId">creator id</param>
        /// <returns></returns>
        Task<PlanWorkout?> GetPlanWorkoutById(Guid id, string userId);

    }
}
