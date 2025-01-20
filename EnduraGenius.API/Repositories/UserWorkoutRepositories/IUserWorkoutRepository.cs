using EnduraGenius.API.Models.Domain;

namespace EnduraGenius.API.Repositories.UserWorkoutRepositories
{
    /// <summary>
    /// Interface for the UserWorkoutRepository
    /// </summary>
    public interface IUserWorkoutRepository
    {
        /// <summary>
        /// Create a new UserWorkout
        /// </summary>
        /// <param name="Workout">workout object</param>
        /// <param name="userId">requested user id</param>
        /// <returns>
        /// new userWorkout object if created successfully
        /// </returns>
        Task<UserWorkout?> CreateUserWorkout(Workout Workout, string userId);

        /// <summary>
        /// Delete a UserWorkout
        /// </summary>
        /// <param name="id">id of the workout to delete</param>
        /// <param name="userId">id of the user</param>
        /// <returns>
        /// true id the workout is deleted successfully otherwise false
        /// </returns>
        Task<bool> DeleteUserWorkout(Guid id, string userId);

        /// <summary>
        /// Get a UserWorkout by id
        /// </summary>
        /// <param name="id">the id of the userWorkout object</param>
        /// <param name="userId">the user id</param>
        /// <returns>
        /// user workout object if found otherwise null
        /// </returns>
        Task<UserWorkout?> GetUserWorkoutById(Guid id, string userId);

        /// <summary>
        /// Get all UserWorkouts
        /// </summary>
        /// <param name="userId">user id</param>
        /// <param name="filterOn">column to filter on</param>
        /// <param name="filterQuery">query to search for</param>
        /// <param name="pageNumber">workouts page number</param>
        /// <param name="pageSize">workout page size</param>
        /// <returns>
        /// list of all userWorkouts
        /// </returns>
        Task<List<UserWorkout>> GetUserWorkoutByUserId(string userId, string? filterOn, string? filterQuery, int pageNumber, int pageSize);

        /// <summary>
        /// Update a UserWorkout
        /// </summary>
        /// <param name="id">the workout id</param>
        /// <param name="userId"> the user id</param>
        /// <param name="MaxWeight">user maximum weight</param>
        /// <param name="LastWeight">user last lefted weight</param>
        /// <param name="TimesPerformed">how many times user performed this workout</param>
        /// <returns>
        /// userworkout object if updated successfully otherwise null
        /// </returns>
        Task<UserWorkout?> UpdateUserWorkout(Guid id, string userId, float? MaxWeight, float? LastWeight, int? TimesPerformed);

        /// <summary>
        /// incrment the times performed for a workout by one
        /// </summary>
        /// <param name="Userid">user id</param>
        /// <param name="WorkoutId">workout id</param>
        /// <returns>
        /// true if incremented successfully otherwise false
        /// </returns>
        Task<bool> AddOneTimesPerformed(string Userid, Guid WorkoutId);

        /// <summary>
        /// decrement the times performed for a workout by one
        /// </summary>
        /// <param name="Userid">user id</param>
        /// <param name="WorkoutId">workout id</param>
        /// <returns>
        /// true if decremented successfully otherwise false
        /// </returns>
        Task<bool> RemoveOneTimesPerformed(string Userid, Guid WorkoutId);

        /// <summary>
        /// get a userworkout object by workout id
        /// </summary>
        /// <param name="userId">user id</param>
        /// <param name="WorkoutId">workout id</param>
        /// <returns>
        /// userworkout object if found otherwise null
        /// </returns>
        Task<UserWorkout?> GetUserWorkoutByWorkoutId(string userId, Guid WorkoutId);
    }
}
