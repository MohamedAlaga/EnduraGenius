

namespace EnduraGenius.API.Repositories.AuthRepository
{
    /// <summary>
    /// Auth Repository Interface
    /// responsible for getting the current user id and role
    /// </summary>
    public interface IAuthRepository
    {
        /// <summary>
        /// Get the current user id
        /// </summary>
        /// <returns>
        /// current user id
        /// </returns>
        String? GetCurrentUserId();

        /// <summary>
        /// Get the current user role
        /// </summary>
        /// <returns>
        /// get the current user roles
        /// </returns>
        String? GetCurrentUserRole();
    }
}
