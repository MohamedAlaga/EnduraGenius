using EnduraGenius.API.Data;
using EnduraGenius.API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace EnduraGenius.API.Repositories.UserRepository
{
    public class SQLUserRepository : IUserRepository
    {
        private readonly EnduraGeniusDBContext _context;
        public SQLUserRepository(EnduraGeniusDBContext context)
        {
            _context = context;
        }
        public async Task<User?> GetUserById(string userId)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
        }
        public async Task<User?> EditUserBodyData(string userId, float? weight, int? tall, int? age, bool? isMale)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null)
            {
                return null;
            }
            user.WeightInKg = weight ?? user.WeightInKg;
            user.TallInCm = tall ?? user.TallInCm;
            user.Age = age ?? user.Age;
            user.IsMale = isMale ?? user.IsMale;
            await _context.SaveChangesAsync();
            return user;
        }
        public async Task<User?> EditUserPoints(string userId, int points)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null)
            {
                return null;
            }
            user.Points = points;
            await _context.SaveChangesAsync();
            return user;
        }
        public async Task<User?> AddUserPoints(string userId, int points)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null)
            {
                return null;
            }
            user.Points += points;
            await _context.SaveChangesAsync();
            return user;
        }
    }
}
