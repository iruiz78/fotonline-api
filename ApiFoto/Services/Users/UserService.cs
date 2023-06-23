using ApiFoto.Domain.User;
using ApiFoto.Infrastructure.Communication;
using ApiFoto.Repository.Generic;
using ApiFoto.Repository.Users;

namespace ApiFoto.Services.Users
{
    public class UserService : IUserService
    {
        private readonly UserRepository _repository;
        public UserService(IGenericRepository<User> repository)
        {
            _repository = (UserRepository)repository;
        }

        public async Task<GenericResponse<UserResponse>> GetAll()
            => new GenericResponse<UserResponse>(await _repository.GetAll());
        
        public async Task<UserResponse> GetByUsername(string username)
            => await _repository.GetByUsername(username);
        
    }
}
