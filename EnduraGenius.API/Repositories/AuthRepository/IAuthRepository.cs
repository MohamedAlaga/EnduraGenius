

namespace EnduraGenius.API.Repositories.AuthRepository
{
    public interface IAuthRepository
    {
        String? GetCurrentUserId();
        String? GetCurrentUserRole();
    }
}
