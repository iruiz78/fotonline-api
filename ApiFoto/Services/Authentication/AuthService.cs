using ApiFoto.Domain;
using ApiFoto.Domain.User;
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

            string mail = tokenExpired.Claims.First(x => x.Type == "Mail").Value;

            var refreshTokenFound = await _repository.GetRefreshTokenByUserId(token.UserId, token.TokenExpired, token.TokenRefresh);
            if (refreshTokenFound == null) return new GenericResponse<AuthResponse>(400, "No existe refresh token", new AuthResponse());

            AuthResponse authentication = new()
            {
                Token = GenerateToken(mail),
                TokenRefresh = GenerateRefreshToken(),
                ExpiredDate = DateTime.UtcNow.AddMinutes(_jwtSettings.Expire),
            };
            await SaveRefreshToken(token.UserId, authentication.Token, authentication.TokenRefresh);
            return new GenericResponse<AuthResponse>(authentication);

        }

        public async Task<GenericResponse<AuthResponse>> Login(AuthRequest auth)
        {
            AuthResponse authentication;
            var user = await _userService.GetByMail(auth.Mail);

            if (user is null) 
            {
                switch (auth.Provider)
                {
                    case (int)Enums.LoginProvider.OWN:
                        return new GenericResponse<AuthResponse>(401, "Unauthorized", new AuthResponse());
                    case (int)Enums.LoginProvider.FACEBOOK:
                        await _userService.Create(new UserRequest() { FullName = auth.FullName, Mail = auth.Mail, Password = "" });
                        break;
                    case (int)Enums.LoginProvider.GOOGLE:
                        await _userService.Create(new UserRequest() { FullName = auth.FullName, Mail = auth.Mail, Password = "" });
                        break;
                }

                user = await _userService.GetByMail(auth.Mail);
            }
            else if (auth.Provider == (int)Enums.LoginProvider.OWN && user.Password != auth.Password)
            {
                return new GenericResponse<AuthResponse>(401, "Unauthorized", new AuthResponse());
            }

            authentication = GenerateCredentials(user.Mail);

            await SaveRefreshToken(user.Id, authentication.Token, authentication.TokenRefresh);
            return new GenericResponse<AuthResponse>(authentication);
        }

        private AuthResponse GenerateCredentials(string mail) 
        {
            AuthResponse authentication = new();

            authentication.Mail = mail;
            authentication.Token = GenerateToken(authentication.Mail);
            authentication.TokenRefresh = GenerateRefreshToken();
            authentication.ExpiredDate = DateTime.UtcNow.AddMinutes(_jwtSettings.Expire);

            return authentication;
        }

        private string GenerateToken(string mail)
        {
            var key = Encoding.ASCII.GetBytes(_jwtSettings.Key);
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] { new Claim("Mail", mail) }),
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
