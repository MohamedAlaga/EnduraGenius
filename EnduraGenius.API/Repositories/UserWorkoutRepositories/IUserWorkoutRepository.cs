using EnduraGenius.API.Models.Domain;

namespace EnduraGenius.API.Repositories.UserWorkoutRepositories
{
    public interface IUserWorkoutRepository
    {
        Task<UserWorkout?> CreateUserWorkout(Workout Workout, string userId);
        Task<bool> DeleteUserWorkout(Guid id);
        Task<UserWorkout?> GetUserWorkoutById(Guid id);
        Task<List<UserWorkout>> GetUserWorkoutByUserId(string userId, string? filterOn, string? filterQuery, int pageNumber, int pageSize);
        Task<UserWorkout?> UpdateUserWorkout(Guid id, float? MaxWeight, float? LastWeight, int? TimesPerformed);
        Task<bool> AddOneTimesPerformed(string Userid, Guid WorkoutId);
        Task<bool> RemoveOneTimesPerformed(string Userid, Guid WorkoutId);
        Task<UserWorkout?> GetUserWorkoutByWorkoutId(string userId, Guid WorkoutId);
    }
}
