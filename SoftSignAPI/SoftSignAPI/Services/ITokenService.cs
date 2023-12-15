using SoftSignAPI.Helpers;
using SoftSignAPI.Model;

namespace SoftSignAPI.Services
{
    public interface ITokenService
    {
        string CreateToken(User user);
        RefreshToken GenerateRefreshToken(User user);
        Task<RefreshToken> SetRefreshToken(User user, HttpResponse response);
    }
}
