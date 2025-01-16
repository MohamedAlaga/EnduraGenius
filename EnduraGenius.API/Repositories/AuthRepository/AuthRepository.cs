
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace EnduraGenius.API.Repositories.AuthRepository
{
    public class AuthRepository : IAuthRepository
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AuthRepository(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public string? GetCurrentUserId()
        {
            var user = _httpContextAccessor.HttpContext?.User;
            if (user == null)
            {
                return null;
            }
            return user.FindFirstValue(ClaimTypes.NameIdentifier);
            
        }

        public string? GetCurrentUserRole()
        {
            var user = _httpContextAccessor.HttpContext?.User;
            if (user == null)
            {
                return null;
            }
            return user.FindFirstValue(ClaimTypes.Role);
        }
    }
}
