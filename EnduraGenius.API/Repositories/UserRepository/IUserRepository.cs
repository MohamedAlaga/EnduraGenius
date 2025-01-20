using EnduraGenius.API.Models.Domain;
using EnduraGenius.API.Models.DTO;

namespace EnduraGenius.API.Repositories.UserRepository
{
    /// <summary>
    /// User Repository interface
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Get user by id
        /// </summary>
        /// <param name="userId">requested user id</param>
        /// <returns>
        /// user object if found
        /// </returns>
        Task<User?> GetUserById(string userId);

        /// <summary>
        /// edit the user body data
        /// </summary>
        /// <param name="userId">user id</param>
        /// <param name="weight">user weight in KG</param>
        /// <param name="tall">user height in cm</param>
        /// <param name="age">user age in years</param>
        /// <param name="isMale">true if user male otherwise flase</param>
        /// <param name="isPublic">true if user profile is set as public</param>
        /// <returns>
        /// new user object if edited
        /// </returns>
        Task<User?> EditUserBodyData(string userId, float? weight, int? tall, int? age, bool? isMale, bool? isPublic);

        /// <summary>
        /// edit user points
        /// </summary>
        /// <param name="userId">user id</param>
        /// <param name="points">new points</param>
        /// <returns>
        /// new user object if edited
        /// </returns>
        Task<User?> EditUserPoints(string userId, int points);

        /// <summary>
        /// add points to user
        /// </summary>
        /// <param name="userId">user id</param>
        /// <param name="points">points to add</param>
        /// <returns>
        /// new user object if edited
        /// </returns>
        Task<User?> AddUserPoints(string userId, int points);

        /// <summary>
        /// get users leaderboard
        /// </summary>
        /// <returns>
        /// ordered list of LeaderBoardResponseDTO by points
        /// </returns>
        Task<List<LeaderBoardResponseDTO>> LeaderBoard();

        /// <summary>
        /// update user profile picture
        /// </summary>
        /// <param name="userId"> user id</param>
        /// <param name="file"> new user profile pic</param>
        /// <returns>
        /// string of the new user profile picture url if updated
        /// </returns>
        Task<string?> UpdateUserPicture(string userId,IFormFile file);
    }
}
