using EnduraGenius.API.Data;
using EnduraGenius.API.Models.Domain;
using EnduraGenius.API.Models.DTO;
using EnduraGenius.API.Repositories.MuscleRepositories;
using Microsoft.EntityFrameworkCore;

namespace EnduraGenius.API.Repositories
{
    public class SQLMuscleRepository : IMuscleRepository
    {
        private readonly EnduraGeniusDBContext _dbcontext;
        public SQLMuscleRepository(EnduraGeniusDBContext dBContext)
        {
            this._dbcontext = dBContext;
        }
        public async Task<Muscle?> CreateMuscle(Muscle muscle)
        {
            try
            {
                await _dbcontext.Muscles.AddAsync(muscle);
                await _dbcontext.SaveChangesAsync();
                return muscle;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<bool> DeleteMuscle(Guid MuscleId)
        {
                var muscle = await _dbcontext.Muscles.FindAsync(MuscleId);
                if (muscle == null)
                {
                    return false;
                }
                var WorkoutsWithMainMuscle = await _dbcontext.Workouts.Where(x => x.MainMuscleId == muscle.Id).ToListAsync();
                foreach (var workout in WorkoutsWithMainMuscle)
                {
                    workout.MainMuscleId = null;
                    _dbcontext.Update(workout);
            }
                await _dbcontext.SaveChangesAsync();
                var WorkoutsWithSecondMuscle = await  _dbcontext.Workouts.Where(x => x.SecondaryMuscleId == muscle.Id).ToListAsync();
                foreach (var workout in WorkoutsWithSecondMuscle)
                {
                    workout.SecondaryMuscleId = null;
                    _dbcontext.Update(workout);
            }
                await _dbcontext.SaveChangesAsync();
                _dbcontext.Muscles.Remove(muscle);
                await _dbcontext.SaveChangesAsync();
                return true;
        }

        public async Task<Muscle?> GetMuscleById(Guid id)
        {
            try
            {
                return await _dbcontext.Muscles.FindAsync(id);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<List<Muscle>> GetMuscles()
        {
            try
            {
                return await _dbcontext.Muscles.ToListAsync();
            }
            catch (Exception)
            {
                return new List<Muscle>();
            }
        }

        public async Task<Muscle?> UpdateMuscle(Muscle OldMuscle,UpdateMuscleDto NewMuscle )
        {
            try
            {
                OldMuscle.Name = NewMuscle.Name ?? OldMuscle.Name;
                OldMuscle.Description = NewMuscle.Description ?? OldMuscle.Description;
                await _dbcontext.SaveChangesAsync();
                return OldMuscle;
            }
            catch
            {
                return null;
            }
        }

        public async Task<Muscle?> GetMuscleByName(string Name)
        {
            try
            {
                return await _dbcontext.Muscles.FirstOrDefaultAsync(x => x.Name == Name);
            }
            catch
            {
                return null;
            }
        }
    }
}
