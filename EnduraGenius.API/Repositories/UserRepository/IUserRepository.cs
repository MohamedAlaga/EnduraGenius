using EnduraGenius.API.Models.Domain;

namespace EnduraGenius.API.Repositories.UserRepository
{
    public interface IUserRepository
    {
        Task<User?> GetUserById(string userId);
        Task<User?> EditUserBodyData(string userId, float? weight, int? tall, int? age, bool? isMale);
        Task<User?> EditUserPoints(string userId, int points);
        Task<User?> AddUserPoints(string userId, int points);
    }
}
