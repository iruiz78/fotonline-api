using ApiFoto.Infrastructure.Auth.Domain;
using ApiFoto.Infrastructure.Communication;
using ApiFoto.Repository.Authentication;
using ApiFoto.Repository.Generic;
using ApiFoto.Services.Users;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace ApiFoto.Services.Authentication
{
    public class AuthService : IAuthService
    {
        private readonly IUserService _userService;
        private readonly JwtSettings _jwtSettings;
        private readonly AuthRepository _repository;
        public AuthService(IUserService userService, IOptions<JwtSettings> jwtSettings, IGenericRepository<Auth> repository)
        {
            _userService = userService;
            _jwtSettings = jwtSettings.Value;
            _repository = (AuthRepository)repository;
        }

        public async Task<GenericResponse<AuthResponse>> GetRefreshToken(RefreshTokenRequest token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenExpired = tokenHandler.ReadJwtToken(token.TokenExpired);

            if (tokenExpired.ValidTo > DateTime.UtcNow)
                return new GenericResponse<AuthResponse>(400, "Token no expirado", new AuthResponse());

            string username = tokenExpired.Claims.First(x => x.Type == "Username").Value;

            var refreshTokenFound = await _repository.GetRefreshTokenByUserId(token.UserId, token.TokenExpired, token.TokenRefresh);
            if (refreshTokenFound == null) return new GenericResponse<AuthResponse>(400, "No existe refresh token", new AuthResponse());

            AuthResponse authentication = new()
            {
                Token = GenerateToken(username),
                TokenRefresh = GenerateRefreshToken(),
                ExpiredDate = DateTime.UtcNow.AddMinutes(_jwtSettings.Expire),
            };
            await SaveRefreshToken(token.UserId, authentication.Token, authentication.TokenRefresh);
            return new GenericResponse<AuthResponse>(authentication);

        }

        public async Task<GenericResponse<AuthResponse>> Login(AuthRequest auth)
        {
            // Validar auth
            AuthResponse authentication = new();
            var user = await _userService.GetByUsername(auth.Username);
            if (user is not null)
            {
                authentication.UserName = user.Username;
                authentication.Token = GenerateToken(authentication.UserName);
                authentication.TokenRefresh = GenerateRefreshToken();
                authentication.ExpiredDate = DateTime.UtcNow.AddMinutes(_jwtSettings.Expire);
            }
            else
            {
                return new GenericResponse<AuthResponse>(401, "Unauthorized", authentication);
            }
            await SaveRefreshToken(user.Id, authentication.Token, authentication.TokenRefresh);
            return new GenericResponse<AuthResponse>(authentication);
        }


        private string GenerateToken(string username)
        {
            var key = Encoding.ASCII.GetBytes(_jwtSettings.Key);
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] { new Claim("Username", username) }),
                Expires = DateTime.UtcNow.AddMinutes(_jwtSettings.Expire),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            });
            return tokenHandler.WriteToken(token);
        }

        private string GenerateRefreshToken()
        {
            var byteArray = new byte[64];
            var refreshToken = string.Empty;

            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(byteArray);
                refreshToken = Convert.ToBase64String(byteArray);
            }
            return refreshToken;
        }

        private async Task SaveRefreshToken(int userId, string token, string refreshToken)
        {
            RefreshToken refresh = new()
            {
                UserId = userId,
                Token = token,
                TokenRefresh = refreshToken,
                ExpiratedDate = DateTime.UtcNow.AddMinutes(_jwtSettings.ExpireRefresh) // Ver esto como queda cuando validamos si esta expirado o no
            };

            await _repository.SaveRefreshToken(refresh);
        }
    }
}
