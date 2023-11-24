using ApiFoto.Infrastructure.Auth.Domain;
using ApiFoto.Infrastructure.Communication;
using ApiFoto.Services.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiFoto.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _service;
        public AuthController(IAuthService service)
        {
            _service = service;
        }

        [Authorize]
        [HttpPost("TestConnectionApi")]
        public async Task TestConnectionApi() 
        {
            Task.Delay(1000).Wait();
        }

        [HttpPost("Login")]
        public async Task<GenericResponse<AuthResponse>> Login(AuthRequest auth)
        => await _service.Login(auth);

        [HttpPost("RefreshToken")]
        public async Task<GenericResponse<AuthResponse>> RefreshToken(RefreshTokenRequest auth)
        => await _service.GetRefreshToken(auth);

        [HttpPost("SendCodeResetPassword")]
        public async Task SendCodeResetPassword(SendCodeRequest sendCodeRequest)
        => await _service.SendCodeResetPassword(sendCodeRequest);

        [HttpPost("ValidateCodeResetPassword")]
        public async Task ValidateCodeResetPassword(ValidateCodeRequest validateCodeRequest)
        => await _service.ValidateCodeResetPassword(validateCodeRequest);

        [HttpPost("ResetPassword")]
        public async Task ResetPassword(ResetPasswordRequest resetPasswordRequest)
        => await _service.ResetPassword(resetPasswordRequest);
    }
}
