using EnduraGenius.API.Models.Domain;
using EnduraGenius.API.Models.DTO;

namespace EnduraGenius.API.Repositories.MuscleRepositories
{
    /// <summary>
    /// Interface for the muscle repository
    /// </summary>
    public interface IMuscleRepository
    {
        /// <summary>
        /// Get all muscles
        /// </summary>
        /// <returns>
        /// list contains all muscles
        /// </returns>
        Task<List<Muscle>> GetMuscles();
        /// <summary>
        /// Get muscle by id
        /// </summary>
        /// <param name="id">id of the muscle requested</param>
        /// <returns>
        /// requested muscle object
        /// </returns>
        Task<Muscle?> GetMuscleById(Guid id);
        /// <summary>
        /// Get muscle by name
        /// </summary>
        /// <param name="Name">name of the muscle</param>
        /// <returns>
        /// requested muscle object
        /// </returns>
        Task<Muscle?> GetMuscleByName(string Name);

        /// <summary>
        /// Create a new muscle
        /// </summary>
        /// <param name="muscle">new muscle object</param>
        /// <returns>
        /// new muscle object if created successfully
        /// </returns>
        Task<Muscle?> CreateMuscle(Muscle muscle);

        /// <summary>
        /// Update a muscle
        /// </summary>
        /// <param name="OldMuscle">old muscle object</param>
        /// <param name="NewMuscle">FTO contains new data</param>
        /// <returns>
        /// muscle object if updated successfully
        /// </returns>
        Task<Muscle?> UpdateMuscle(Muscle OldMuscle, UpdateMuscleDto NewMuscle);

        /// <summary>
        /// Delete a muscle
        /// </summary>
        /// <param name="MuscleId">id of the muscle to delete</param>
        /// <returns>
        /// true if delete muscle succefully
        /// </returns>
        Task<bool> DeleteMuscle(Guid MuscleId);
    }
}
