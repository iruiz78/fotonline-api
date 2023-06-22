using ApiFoto.Domain.User;
using ApiFoto.Infrastructure.Communication;
using ApiFoto.Services.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiFoto.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;
        public UserController(IUserService service) 
        {
            _service = service;
        }

        [Authorize]
        [HttpGet("GetAll")]
        public async Task<GenericResponse<UserResponse>> GetAll()
            => await _service.GetAll();
    }
}
