using EnduraGenius.API.Data;
using EnduraGenius.API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace EnduraGenius.API.Repositories.PlanWorkoutsRepositories
{
    public class SQLPlanWorkoutRepository : IPlanWorkoutsRepository
    {
        private readonly EnduraGeniusDBContext _dbcontext;
        public SQLPlanWorkoutRepository(EnduraGeniusDBContext enduraGeniusDB)
        {
            _dbcontext = enduraGeniusDB;
        }
        public async Task<PlanWorkout?> CreatePlanWorkout(Plan plan, Workout Workout, string Reps, int dayNumber, int order)
        {
            try
            {
                Console.WriteLine("new workout : "+Workout.Id);
                var workoutplan = new PlanWorkout
                {
                    Plan = plan,
                    Workout = Workout,
                    Reps = Reps,
                    DayNumber = dayNumber,
                    Order = order
                };
                await _dbcontext.PlanWorkouts.AddAsync(workoutplan);
                await _dbcontext.SaveChangesAsync();
                return workoutplan;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<bool> DeletePlanWorkout(Guid id, string userId)
        {
            var workoutplan = await _dbcontext.PlanWorkouts.Include(x => x.Plan).Where(x => x.Id == id && x.Plan.PlanCreatedBy == userId).FirstOrDefaultAsync();
            if (workoutplan == null)
            {
                return false;
            }
            _dbcontext.PlanWorkouts.Remove(workoutplan);
            await _dbcontext.SaveChangesAsync();
            return true;
        }

        public async Task<List<PlanWorkout>> GetPlanWorkoutByPlanId(Guid id, string userId)
        {
            return await _dbcontext.PlanWorkouts.
                Include(x=> x.Workout)
                .Include(x => x.Workout.MainMuscle)
                .Include(x => x.Workout.SecondaryMuscle)
                .Include(x=>x.Plan)
                .Where(x => x.PlanId == id)
                .Where(x => x.Plan.PlanCreatedBy == userId || x.Plan.IsPublic == true)
                .OrderBy(x => x.DayNumber)
                .ToListAsync();
        }

        public async Task<PlanWorkout?> GetPlanWorkoutById(Guid id , string userId)
        {
            return await _dbcontext.PlanWorkouts.Include(x => x.Workout)
                .Include(x => x.Workout.MainMuscle)
                .Include(x => x.Workout.SecondaryMuscle)
                .Include(x => x.Plan)
                .Where(x => x.Id == id)
                .Where(x => x.Plan.PlanCreatedBy == userId || x.Plan.IsPublic == true)
                .FirstOrDefaultAsync();
        }

        public async Task<PlanWorkout?> UpdatePlanWorkout(Guid id,string userId, Guid? NewWorkoutId, string? Reps, int? dayNumber, int? Order)
        {
            var workoutplan = await _dbcontext.PlanWorkouts.Include(x => x.Plan).Where(x => x.Id == id && x.Plan.PlanCreatedBy == userId).FirstOrDefaultAsync();
            if (workoutplan == null)
            {
                return null;
            }
            if (NewWorkoutId != workoutplan.WorkoutId && NewWorkoutId != null)
            {
                var workout = await _dbcontext.Workouts.FindAsync(NewWorkoutId);
                if (workout == null)
                {
                    return null;
                }
                workoutplan.WorkoutId = workout.Id;
            }
            if (Reps != null)
            {
                workoutplan.Reps = Reps;
            }
            if (dayNumber != null)
            {
                workoutplan.DayNumber = dayNumber.Value;
            }
            if (Order != workoutplan.Order && Order != null)
            {
                var workoutsInDay = await _dbcontext.PlanWorkouts
                    .Where(x => x.PlanId == workoutplan.PlanId && x.DayNumber == workoutplan.DayNumber)
                    .OrderBy(x => x.Order)
                    .ToListAsync();
                workoutsInDay.Remove(workoutplan);
                workoutsInDay.Insert(Order.Value - 1, workoutplan);
                for (int i = 0; i < workoutsInDay.Count; i++)
                {
                    workoutsInDay[i].Order = i + 1;
                }
                _dbcontext.PlanWorkouts.UpdateRange(workoutsInDay);
            }
            await _dbcontext.SaveChangesAsync();

            return workoutplan;
        }
    }
}
