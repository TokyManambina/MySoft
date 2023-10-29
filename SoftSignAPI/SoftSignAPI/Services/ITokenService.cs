using SoftSignAPI.Helpers;
using SoftSignAPI.Model;

namespace SoftSignAPI.Services
{
    public interface ITokenService
    {
        string CreateToken(User user);
        RefreshToken GenerateRefreshToken(User user);
        void SetRefreshToken(User user, HttpResponse response);
    }
}
