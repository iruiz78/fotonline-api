using ApiFoto.Domain.Settings.Modules;
using ApiFoto.Domain.Settings.Rols;
using ApiFoto.Domain.User;
using ApiFoto.Infrastructure.Communication;

namespace ApiFoto.Services.Users
{
    public interface IUserService
    {
        Task<GenericResponse<UserResponse>> GetAll();
        Task<GenericResponse<UserResponse>> GetByMail(string mail);
        Task<GenericResponse<UserResponse>> Save(UserRequest userRequest);
        GenericResponse<Module> GetAllModules();
        GenericResponse<Rol> GetAllRols();
        Task Disable(UserResponse user);
        Task<GenericResponse<UserResponse>> GetById(int id);
        
    }
}