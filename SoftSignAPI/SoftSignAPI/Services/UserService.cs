using System.Security.Claims;

namespace SoftSignAPI.Services
{
    public class UserService : IUserService
    {
        public readonly IHttpContextAccessor _httpContextAccessor;
        public UserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetMail()
        {
            if (_httpContextAccessor.HttpContext == null)
                return string.Empty;

            return _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Email);
        }

        public string GetRole()
        {
            if (_httpContextAccessor.HttpContext == null)
                return string.Empty;

            return _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Role);
        }
    }
}
