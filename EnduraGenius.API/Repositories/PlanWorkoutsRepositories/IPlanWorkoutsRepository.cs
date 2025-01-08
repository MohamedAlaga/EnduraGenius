using EnduraGenius.API.Models.Domain;

namespace EnduraGenius.API.Repositories.PlanWorkoutsRepositories
{
    public interface IPlanWorkoutsRepository
    {
        Task<PlanWorkout?> CreatePlanWorkout(Plan plan, Workout Workout, string Reps, int dayNumber, int order);
        Task<bool> DeletePlanWorkout(Guid id);
        Task<List<PlanWorkout>> GetPlanWorkoutByPlanId(Guid id);
        Task<PlanWorkout?> UpdatePlanWorkout(Guid id, Guid? NewWorkoutId, string? Reps, int? dayNumber, int? Order);
        Task<PlanWorkout?> GetPlanWorkoutById(Guid id);

    }
}
