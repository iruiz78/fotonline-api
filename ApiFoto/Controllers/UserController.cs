using ApiFoto.Domain.Settings.Modules;
using ApiFoto.Domain.Settings.Rols;
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

        //[Authorize]
        [HttpGet("GetAll")]
        public async Task<GenericResponse<UserResponse>> GetAll()
            => await _service.GetAll();

        [HttpPost("Save")]
        public async Task<GenericResponse<UserResponse>> Save(UserRequest user)
            => await _service.Save(user);

        [HttpGet("GetAllModules")]
        public GenericResponse<Module> GetAllModules()
            => _service.GetAllModules();

        [HttpGet("GetAllRols")]
        public GenericResponse<Rol> GetAllRols()
            => _service.GetAllRols();

        [HttpGet("GetById/{id}")]
        public async Task<GenericResponse<UserResponse>> GetById(int id)
            => await _service.GetById(id);

        [HttpPost("Disable")]
        public async Task Disable(UserResponse userResponse)
            => await _service.Disable(userResponse);

    }
}
