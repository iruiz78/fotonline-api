using ApiFoto.Domain.Settings.Modules;
using ApiFoto.Domain.Settings.Rols;
using ApiFoto.Domain.User;
using ApiFoto.Helpers;
using ApiFoto.Infrastructure.Communication;
using ApiFoto.Infrastructure.Communication.Exceptions;
using ApiFoto.Infrastructure.ULogged;
using ApiFoto.Repository.Generic;
using ApiFoto.Repository.Users;
using AutoMapper;
using Microsoft.Extensions.Options;

namespace ApiFoto.Services.Users
{
    public class UserService : IUserService
    {
        private readonly UserRepository _repository;
        private readonly IMapper _mapper;
        private readonly ModulesSettings _modules;
        private readonly List<Rol> _rols;
        private readonly IUserLogged _userLogged;
        public UserService(IGenericRepository<User> repository, IMapper mapper, IOptions<ModulesSettings> modules, IOptions<RolsSettings> rols, IUserLogged userLogged)
        {
            _repository = (UserRepository)repository;
            _mapper = mapper;
            _modules = modules.Value;
            _rols = rols.Value.Rols;
            _userLogged = userLogged;
        }

        public async Task<GenericResponse<UserResponse>> GetAll()
        {
            var userrr = _userLogged.User;
            await TestLifeCicle();
            var users = await _repository.GetAll();
            foreach (var user in users)
            {
                user.Rol = _rols.Where(x => x.Id == user.RolId).FirstOrDefault().Name;
            }
            return new GenericResponse<UserResponse>(users);

        }

        public async Task TestLifeCicle()
        {
            var oo = _userLogged.User;

            //Thread.Sleep(5000);
        }

        public async Task<GenericResponse<UserResponse>> GetByMail(string mail)
            => new GenericResponse<UserResponse>(await _repository.GetByEmail(mail));

        public async Task<GenericResponse<UserResponse>> Save(UserRequest userRequest)
        {
            // Mismas validaciones que con el front... 
            if(userRequest.Id == 0)
            {
                // Password no puede ser null aca
                var exist = await GetByEmail(userRequest.Email);
                if (exist is not null) throw new AppException("El Correo ingresado está en uso.");
                await ValidateNew(userRequest);
                await _repository.Create(userRequest);
                return new GenericResponse<UserResponse>("Usuario creado correctamente.");
            }
            else
            {
                await Validate(userRequest);
                await _repository.Update(userRequest);
                return new GenericResponse<UserResponse>("Usuario actualizado correctamente.");
            }
        }

        private async Task Validate(UserRequest user)
        {
            if(!Utils.ValidEmail(user.Email)) throw new AppException("Formato de correo inválido.");
            if(_rols.Where(x => x.Id == user.RolId).FirstOrDefault() is null) throw new AppException("Formato de Rol inválido.");
        }

        private async Task ValidateNew(UserRequest user)
        {
            if (!Utils.ValidEmail(user.Email)) throw new AppException("Formato de correo inválido.");
            if (string.IsNullOrWhiteSpace(user.Password)) throw new AppException("Formato de contraseña inválido.");
            if (_rols.Where(x => x.Id == user.RolId).FirstOrDefault() is null) throw new AppException("Formato de Rol inválido.");
        }

        private async Task<UserResponse> GetByEmail(string email)
            => await _repository.GetByEmail(email);

        public GenericResponse<Module> GetAllModules()
            => new(_modules.Modules);

        public GenericResponse<Rol> GetAllRols()
            => new(_rols);

        public async Task Disable(UserResponse user)
        {
            if(user == null || user.Id <= 0) throw new AppException("Error al identificar el usuario.");
            await _repository.Disable(user.Id);
        }

        public async Task<GenericResponse<UserResponse>> GetById(int id)
        {
            if (id <= 0) throw new AppException("Identificador de usuario inválido.");
            var user = await _repository.GetById(id);
            return user is null
                ? throw new AppException($"Usuario con identificador: {id}, no se encuentra activo o no existe.")
                : new GenericResponse<UserResponse>(user);
        }
    }
}
