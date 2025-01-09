using EnduraGenius.API.Models.Domain;

namespace EnduraGenius.API.Repositories.PlanWorkoutsRepositories
{
    public interface IPlanWorkoutsRepository
    {
        Task<PlanWorkout?> CreatePlanWorkout(Plan plan, Workout Workout, string Reps, int dayNumber, int order);
        Task<bool> DeletePlanWorkout(Guid id, string userId);
        Task<List<PlanWorkout>> GetPlanWorkoutByPlanId(Guid id, string userId);
        Task<PlanWorkout?> UpdatePlanWorkout(Guid id, string userId, Guid? NewWorkoutId, string? Reps, int? dayNumber, int? Order);
        Task<PlanWorkout?> GetPlanWorkoutById(Guid id, string userId);

    }
}
