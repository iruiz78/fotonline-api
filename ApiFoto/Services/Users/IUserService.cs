using ApiFoto.Domain.User;
using ApiFoto.Infrastructure.Communication;

namespace ApiFoto.Services.Users
{
    public interface IUserService
    {
        Task<GenericResponse<UserResponse>> GetAll();
        Task<UserResponse> GetByMail(string mail);
        Task Create(UserRequest userRequest);
    }
}