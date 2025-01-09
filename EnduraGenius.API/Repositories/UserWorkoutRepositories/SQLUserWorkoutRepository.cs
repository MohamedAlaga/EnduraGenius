using EnduraGenius.API.Data;
using EnduraGenius.API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace EnduraGenius.API.Repositories.UserWorkoutRepositories
{
    public class SQLUserWorkoutRepository : IUserWorkoutRepository
    {
        private readonly EnduraGeniusDBContext _enduraGeniusDBContext;
        public SQLUserWorkoutRepository(EnduraGeniusDBContext enduraGeniusDBContext)
        {
            _enduraGeniusDBContext = enduraGeniusDBContext;
        }
        public async Task<UserWorkout?> CreateUserWorkout(Workout Workout, string userId)
        {
            var UserWorkout = await _enduraGeniusDBContext.UserWorkouts.FirstOrDefaultAsync(x => x.UserId == userId && x.WorkoutId == Workout.Id);
            if (UserWorkout != null)
            {
                return null;
            }
            var NewUserWorkout = new UserWorkout
            {
                UserId = userId,
                WorkoutId = Workout.Id,
                MaxWeight = 0,
                LastWeight = 0
            };
            await _enduraGeniusDBContext.UserWorkouts.AddAsync(NewUserWorkout);
            await _enduraGeniusDBContext.SaveChangesAsync();
            return NewUserWorkout;
        }
        public async Task<bool> DeleteUserWorkout(Guid id, string userId)
        {
            var UserWorkout = await _enduraGeniusDBContext.UserWorkouts.FindAsync(id);
            if (UserWorkout == null || UserWorkout.UserId != userId)
            {
                return false;
            }
            _enduraGeniusDBContext.UserWorkouts.Remove(UserWorkout);
            await _enduraGeniusDBContext.SaveChangesAsync();
            return true;
        }
        public async Task<UserWorkout?> GetUserWorkoutById(Guid id, string userId)
        {
            return await _enduraGeniusDBContext.UserWorkouts.Where(x => x.Id == id && x.UserId == userId).FirstOrDefaultAsync();
        }
        public async Task<List<UserWorkout>> GetUserWorkoutByUserId(string userId, string? filterOn, string? filterQuery, int pageNumber, int pageSize)
        {
            var workouts = _enduraGeniusDBContext.UserWorkouts
                .Where(x => x.UserId == userId)
                .Include(x => x.User)
                .Include(x => x.Workout)
                .AsQueryable();

            if (string.IsNullOrWhiteSpace(filterOn) == false && string.IsNullOrWhiteSpace(filterQuery) == false)
            {
                filterOn = filterOn.ToLower();
                switch (filterOn)
                {
                    case "v":
                        workouts = workouts.Where(x => x.Workout.Name.Contains(filterQuery));
                        break;
                    case "mainmuscle":
                        workouts = workouts.Where(x => x.Workout.MainMuscle.Name.Contains(filterQuery));
                        break;
                    case "secondarymuscle":
                        workouts = workouts.Where(x => x.Workout.SecondaryMuscle.Name.Contains(filterQuery));
                        break;
                    default:
                        return new List<UserWorkout>();
                }
            }

            return await workouts.ToListAsync();
        }
        public async Task<UserWorkout?> GetUserWorkoutByWorkoutId(string userId, Guid WorkoutId)
        {
            return await _enduraGeniusDBContext.UserWorkouts.Include(x => x.Workout).Where(x => x.UserId == userId && x.WorkoutId == WorkoutId).FirstOrDefaultAsync();
        }
        public async Task<UserWorkout?> UpdateUserWorkout(Guid id, string userId, float? MaxWeight, float? LastWeight,int? TimesPerformed)
        {
            var UserWorkout = await _enduraGeniusDBContext.UserWorkouts.Where(x => x.WorkoutId == id && x.UserId == userId).FirstOrDefaultAsync();
            if (UserWorkout == null )
            {
                return null;
            }
            UserWorkout.MaxWeight = MaxWeight ?? UserWorkout.MaxWeight;
            UserWorkout.LastWeight = LastWeight ?? UserWorkout.LastWeight;
            UserWorkout.TimesPerformed = TimesPerformed ?? UserWorkout.TimesPerformed;
            await _enduraGeniusDBContext.SaveChangesAsync();
            return UserWorkout;
        }
        public async Task<bool> AddOneTimesPerformed(string Userid, Guid WorkoutId)
        {
            var UserWorkout = await _enduraGeniusDBContext.UserWorkouts.FirstOrDefaultAsync(x => x.UserId == Userid && x.WorkoutId == WorkoutId);
            if (UserWorkout == null)
            {
                return false;
            }
            UserWorkout.TimesPerformed++;
            await _enduraGeniusDBContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> RemoveOneTimesPerformed(string Userid, Guid WorkoutId)
        {
            var UserWorkout = await _enduraGeniusDBContext.UserWorkouts.FirstOrDefaultAsync(x => x.UserId == Userid && x.WorkoutId == WorkoutId);
            if (UserWorkout == null)
            {
                return false;
            }
            UserWorkout.TimesPerformed--;
            await _enduraGeniusDBContext.SaveChangesAsync();
            return true;
        }
    }
}
