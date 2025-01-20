using EnduraGenius.API.Models.Domain;
using EnduraGenius.API.Models.DTO;

namespace EnduraGenius.API.Repositories.WorkoutsRepositories
{
    /// <summary>
    /// Interface for the workouts repository
    /// </summary>
    public interface IWorkoutsRepository
    {
        /// <summary>
        /// Get a workout by its id
        /// </summary>
        /// <param name="id">workout id</param>
        /// <returns>
        /// workout object if found, null otherwise
        /// </returns>
        Task<Workout?> GetWorkoutById(Guid id);

        /// <summary>
        /// Get all the workouts
        /// </summary>
        /// <param name="filterOn">column to filter on</param>
        /// <param name="filterQuery">queary to search for</param>
        /// <param name="pageNumber">page number</param>
        /// <param name="pageSize">number of records in each page</param>
        /// <param name="IsCertified">certification type to seach for</param>
        /// <returns>
        /// list of workouts that match the search criteria
        /// </returns>
        Task<List<Workout>> GetWorkouts(string? filterOn, string? filterQuery, int pageNumber, int pageSize, bool IsCertified);

        /// <summary>
        /// creat a new workout
        /// </summary>
        /// <param name="workout">the new workout</param>
        /// <param name="MainMuscleId">main muscle id</param>
        /// <param name="SEcondaryMuscleId">secondary muscle id</param>
        /// <param name="UserId">creator id</param>
        /// <returns></returns>
        Task<Workout?> CreateWorkout(Workout workout, Muscle MainMuscleId, Muscle SEcondaryMuscleId, string UserId);

        /// <summary>
        /// update a workout
        /// </summary>
        /// <param name="workout">workout object</param>
        /// <param name="updateWorkoutDto">DTO contians new data</param>
        /// <returns>
        /// true if updated, false otherwise
        /// </returns>
        Task<bool> UpdateWorkout(Workout workout, GetWorkoutDto updateWorkoutDto);

        /// <summary>
        /// delete a workout
        /// </summary>
        /// <param name="id">the id of the workout to delete</param>
        /// <returns>
        /// true if deleted, false otherwise
        /// </returns>
        Task<bool> DeleteWorkout(Guid id);

        /// <summary>
        /// change the certification status of a workout
        /// </summary>
        /// <param name="id">id of the workout to delete</param>
        /// <returns>
        /// workout object if updated, null otherwise
        /// </returns>
        Task<Workout?> ChangeCertificationStatus(Guid id);
    }
}
