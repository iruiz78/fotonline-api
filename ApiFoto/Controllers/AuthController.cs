using ApiFoto.Infrastructure.Auth.Domain;
using ApiFoto.Infrastructure.Communication;
using ApiFoto.Services.Authentication;
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

        [HttpPost("Login")]
        public async Task<GenericResponse<AuthResponse>> Login(AuthRequest auth)
        => await _service.Login(auth);
        

        [HttpPost("RefreshToken")]
        public async Task<GenericResponse<AuthResponse>> RefreshToken(RefreshTokenRequest auth)
        => await _service.GetRefreshToken(auth);
    }
}
