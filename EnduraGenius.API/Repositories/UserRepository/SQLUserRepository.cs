using AutoMapper;
using EnduraGenius.API.Data;
using EnduraGenius.API.Models.Domain;
using EnduraGenius.API.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace EnduraGenius.API.Repositories.UserRepository
{
    public class SQLUserRepository : IUserRepository
    {
        private readonly EnduraGeniusDBContext _context;
        private readonly IMapper _mapper;
        public SQLUserRepository(EnduraGeniusDBContext context,IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }
        public async Task<User?> GetUserById(string userId)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
        }
        public async Task<User?> EditUserBodyData(string userId, float? weight, int? tall, int? age, bool? isMale, bool? isPublic)
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
            user.isPublic = isPublic ?? user.isPublic;
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

        public async Task<List<LeaderBoardResponseDTO>> LeaderBoard()
        {
            var users = await _context.Users.Where(x => x.isPublic == true).OrderByDescending(u => u.Points).Take(10).ToListAsync();
            return _mapper.Map<List<LeaderBoardResponseDTO>>(users);
        }
    }
}
