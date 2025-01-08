using EnduraGenius.API.Models.Domain;
using EnduraGenius.API.Models.DTO;

namespace EnduraGenius.API.Repositories.WorkoutsRepositories
{
    public interface IWorkoutsRepository
    {
        Task<Workout?> GetWorkoutById(Guid id);
        Task<List<Workout>> GetWorkouts(string? filterOn, string? filterQuery, int pageNumber, int pageSize, bool IsCertified);
        Task<Workout?> CreateWorkout(Workout workout, Muscle MainMuscleId, Muscle SEcondaryMuscleId, string UserId);
        Task<bool> UpdateWorkout(Workout workout, GetWorkoutDto updateWorkoutDto);
        Task<bool> DeleteWorkout(Guid id);
        Task<Workout?> ChangeCertificationStatus(Guid id);
    }
}
