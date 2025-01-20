using EnduraGenius.API.Models.Domain;

namespace EnduraGenius.API.Repositories.TokenRepositories
{
    /// <summary>
    /// Interface for the token repository
    /// </summary>
    public interface ITokenRepository
    {
        /// <summary>
        /// Create a JWT token for the user
        /// </summary>
        /// <param name="user">user id</param>
        /// <param name="roles">user roles</param>
        /// <returns>
        /// user JWT token
        /// </returns>
        string CreateJWTToken(User user, List<string> roles);
    }
}
