using ApiFoto.Infrastructure.Auth.Domain;
using ApiFoto.Infrastructure.Communication;

namespace ApiFoto.Services.Authentication
{
    public interface IAuthService
    {
        Task<GenericResponse<AuthResponse>> Login(AuthRequest auth);
        Task<GenericResponse<AuthResponse>> GetRefreshToken(RefreshTokenRequest token);
        Task SendCodeResetPassword(SendCodeRequest sendCodeRequest);
        Task ValidateCodeResetPassword(ValidateCodeRequest validateCodeRequest);
        Task ResetPassword(ResetPasswordRequest resetPasswordRequest);
    }
}