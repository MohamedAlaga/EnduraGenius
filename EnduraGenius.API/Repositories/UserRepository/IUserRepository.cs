using EnduraGenius.API.Models.Domain;
using EnduraGenius.API.Models.DTO;

namespace EnduraGenius.API.Repositories.UserRepository
{
    public interface IUserRepository
    {
        Task<User?> GetUserById(string userId);
        Task<User?> EditUserBodyData(string userId, float? weight, int? tall, int? age, bool? isMale, bool? isPublic);
        Task<User?> EditUserPoints(string userId, int points);
        Task<User?> AddUserPoints(string userId, int points);
        Task<List<LeaderBoardResponseDTO>> LeaderBoard();
    }
}
