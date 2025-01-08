using EnduraGenius.API.Models.Domain;
using EnduraGenius.API.Models.DTO;

namespace EnduraGenius.API.Repositories.MuscleRepositories
{
    public interface IMuscleRepository
    {
        Task<List<Muscle>> GetMuscles();
        Task<Muscle?> GetMuscleById(Guid id);
        Task<Muscle?> GetMuscleByName(string Name);
        Task<Muscle?> CreateMuscle(Muscle muscle);
        Task<Muscle?> UpdateMuscle(Muscle OldMuscle, UpdateMuscleDto NewMuscle);
        Task<bool> DeleteMuscle(Guid MuscleId);
    }
}
