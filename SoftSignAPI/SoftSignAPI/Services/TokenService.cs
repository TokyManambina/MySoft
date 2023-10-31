using Microsoft.IdentityModel.Tokens;
using SoftSignAPI.Helpers;
using SoftSignAPI.Interfaces;
using SoftSignAPI.Model;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace SoftSignAPI.Services
{
    public class TokenService : ITokenService
    {
        private readonly IUserRepository _userRepository;

        public TokenService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public string CreateToken(User user)
        {
            var hashkey = user.Id.GetHashCode().ToString("X");

            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Hash, hashkey),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, nameof(user.Role)),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Tools.MySecret));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: credentials
            );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        public RefreshToken GenerateRefreshToken(User user)
        {

            return new RefreshToken
            {
                Token = /*Cipher.Encrypt(user.Mail, "mail") + " " +*/ Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                Created = DateTime.Now,
                Expires = DateTime.Now.AddDays(7),
            };
        }

        public async Task<RefreshToken> SetRefreshToken(User user, HttpResponse response)
        {
            var newrefreshToken= GenerateRefreshToken(user);
            var coockieOption = new CookieOptions
            {
                HttpOnly = true,
                Expires = newrefreshToken.Expires,
            };

            response.Cookies.Append("refreshToken", newrefreshToken.Token, coockieOption);

            await _userRepository.UpdateToken(user.Id, newrefreshToken);

            return newrefreshToken;
        }
    }
}
