using Goods.System.Social.Network.DAL.Repository.Interface;
using Goods.System.Social.Network.DomainModel.Entities;
using Goods.System.Social.Network.DomainServices.Interface;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NLog;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Goods.System.Social.Network.DomainServices.Realization
{
    public class JWTService : IJWTService
    {
        private readonly JWTSettings _options;
        private readonly IUserRepository _userRepository;
        private readonly IPhotoRepository _avatarRepository;
        private readonly ILogger<JWTService> _logger;
        public JWTService(IOptions<JWTSettings> optAccess, 
                            IUserRepository userRepository,
                            IPhotoRepository avatarRepository,
                            ILogger<JWTService> logger)
        {
            _options = optAccess.Value;
            _userRepository = userRepository;
            _avatarRepository = avatarRepository;
            _logger = logger;
        }
        public string GetJWT(User user)
        {
            _logger.LogTrace($"Вызван метод GetJWT с параметрами: user : {user}");

            List<Claim> claims = new List<Claim>
            {
                new Claim("id", user.Id.ToString()),
                new Claim("firstName", user.FirstName),
                new Claim("lastName", user.LastName),
                new Claim("email", user.Email),
                new Claim("country", user.Country),
                new Claim("city", user.City),
                new Claim("settingNotificationId", user.SettingNotificationId.ToString()),
                new Claim("settingPrivacyId", user.SettingPrivacyId.ToString()),
                new Claim("phone", user.Phone),
                new Claim("dateOfBirth", user.DateBirthday.ToString()),
                new Claim("status", user.Status),
                new Claim("avatar", user.Avatar),
                new Claim("background", user.Background)
            };

            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey));

            var jwt = new JwtSecurityToken(
                issuer: _options.Issuer,
                audience: _options.Audience,
                claims: claims,
                expires: DateTime.UtcNow.Add(TimeSpan.FromHours(1)),
                notBefore: DateTime.UtcNow,
                signingCredentials: new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256)
             );
            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }

        public async Task<string> AuthAsync(string email, string password)
        {
            _logger.LogTrace($"Вызван метод AuthAsync с параметрами: email : {email}, password: {password}");
            var users = await _userRepository.GetAllAsync();
            var user = users.Where(u => u.Email == email).ToList().FirstOrDefault();

            var sha1 = SHA1.Create();
            var shaPass = sha1.ComputeHash(Encoding.Unicode.GetBytes(password));
            if (user is not null && user.Password == Encoding.Unicode.GetString(shaPass))
            {
                return GetJWT(user);
            }
            return "401";
        }

        public async Task<string> RegistrationAsync(User user)
        {
            _logger.LogTrace($"Вызван метод RegistrationAsync с параметрами: user : {user}");
            var usersWithoutUser = await _userRepository.GetAllAsync();
            bool metkaEmail = false;
            foreach (var item in usersWithoutUser)
            {
                if (item.Email == user.Email)
                {
                    metkaEmail = true; break;
                }
            }
            if (!metkaEmail)
            {
                _userRepository.Create(user);
                var users = await _userRepository.GetAllAsync();
                user = users.Where(u => u.Email == user.Email).ToList().FirstOrDefault();
                return GetJWT(user);
            }
            return "Email занят!";
        }

        public async Task<string> UpdateTokenAsync(int userId)
        {
            _logger.LogTrace($"Вызван метод UpdateTokenAsync с параметрами: userId : {userId}");
            var user = await _userRepository.GetAsync(userId);
            return GetJWT(user);
        }
    }
}
