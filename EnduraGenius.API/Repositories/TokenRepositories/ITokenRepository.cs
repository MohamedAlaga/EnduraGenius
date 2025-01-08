using EnduraGenius.API.Models.Domain;

namespace EnduraGenius.API.Repositories.TokenRepositories
{
    public interface ITokenRepository
    {
        string CreateJWTToken(User user, List<string> roles);
    }
}
