using ApiFoto.Domain.User;
using ApiFoto.Infrastructure.Communication;
using ApiFoto.Infrastructure.Communication.Exceptions;
using ApiFoto.Repository.Generic;
using ApiFoto.Repository.Users;
using AutoMapper;

namespace ApiFoto.Services.Users
{
    public class UserService : IUserService
    {
        private readonly UserRepository _repository;
        private readonly IMapper _mapper;
        public UserService(IGenericRepository<User> repository, IMapper mapper)
        {
            _repository = (UserRepository)repository;
            _mapper = mapper;
        }
        public async Task<GenericResponse<UserResponse>> GetAll()
            => new GenericResponse<UserResponse>(await _repository.GetAll());
        
        public async Task<UserResponse> GetByMail(string mail)
            => await _repository.GetByMail(mail);

        public async Task Create(UserRequest userRequest)
        {
            var exist = await _repository.GetByMail(userRequest.Mail);

            if (exist is not null)
                throw new AppException("El Correo ingresado está en uso.");

            await _repository.Create(userRequest);
        }
    }
}
