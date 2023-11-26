using ApiFoto.Domain.Settings.Modules;
using ApiFoto.Domain.User;
using ApiFoto.Infrastructure.Dapper;
using ApiFoto.Repository.Generic;
using AutoMapper;
using Dapper;
using Loggin;
using Microsoft.Extensions.Options;
using System.Data;

namespace ApiFoto.Repository.Users
{
    public class UserRepository : GenericRepository<User>
    {
        private readonly IMapper _mapper;
        
        public UserRepository(DapperContext context, IMapper mapper, IOptions<Log> log) : base(context, log)
        {
            _mapper = mapper;
        }

        // Aca se podria ver si conviene agregar la consulta de los modulos de una o dos endpoint separados con los datos basicos de los usuarios y su rol y otro con todo para algo en particular
        public async new Task<IEnumerable<UserResponse>> GetAll()
        {
            using (var conn = _context.CreateConnectionSQL())
            {
                IEnumerable<UserResponse> users = await conn.QueryAsync<UserResponse>("SELECT * FROM Users WHERE Active = 1");
                foreach (var user in users)
                {
                    user.Modules = await GetUsersModulesByUserId(user.Id, conn);
                }
                return users;
            }
        }

        public async Task Disable(int id)
        {
            int userModifiedId = 9; // Agregar aca lo de la captura del usuario logueado con el JWT
            using (var conn = _context.CreateConnectionSQL())
            {
                await conn.ExecuteAsync("UPDATE Users SET Active = 0 WHERE Id = @Id", new { Id = id, UserModified = DateTime.Now, UserModifiedId = userModifiedId });
            }
        }

        public async Task<UserResponse> GetById(int id)
        {
            using (var conn = _context.CreateConnectionSQL())
            {
                var user = await conn.QueryFirstOrDefaultAsync<UserResponse>("SELECT * FROM Users WHERE Active = 1 AND Id = @Id", new { Id = id });
                if (user is null) return null;
                user.Modules = await GetUsersModulesByUserId(user.Id, conn);
                return user;
            }
        }


        public async Task<UserResponse> GetByEmail(string email)
        {
            using (var conn = _context.CreateConnectionSQL())
            {
                var user = await conn.QueryFirstOrDefaultAsync<UserResponse>("SELECT * FROM Users WHERE Email = @Email AND Active = 1", new { Email = email });
                if (user is null) return null;
                user.Modules = await GetUsersModulesByUserId(user.Id, conn);
                return user;
            }
        }

        public async Task Create(UserRequest userRequest)
        {
            try
            {
                var user = _mapper.Map<User>(userRequest);
                user.CreatedDate = DateTime.Now;
                user.UserCreatedId = 1;
                user.ModifiedDate = DateTime.Now;
                user.UserModifiedId = 1;
                user.Active = true;

                using (var conn = _context.CreateConnectionSQL())
                {
                    conn.Open();
                    using (var transaction = conn.BeginTransaction())
                    {
                        int userId = await conn.ExecuteScalarAsync<int>(GenerateInsertQuery(), user, transaction);
                        await InsertUserModules(userRequest.Modules, userId, conn, transaction);
                        transaction.Commit();
                    }
                }
            }
            catch (Exception ex)
            {
                await _log.AddError($"Error en creacion de usuario - Create - Ex: {ex.Message} - Stack: {ex.StackTrace}");
                throw;
            }
        }

        public async Task Update(UserRequest userRequest)
        {
            try
            {
                var user = _mapper.Map<UserRequestUpdate>(userRequest);
                user.ModifiedDate = DateTime.Now;
                user.UserModifiedId = 1;

                using (var conn = _context.CreateConnectionSQL())
                {
                    conn.Open();
                    using (var transaction = conn.BeginTransaction())
                    {
                        await conn.ExecuteScalarAsync<int>(GenerateUpdateQuery("Users", typeof(UserRequestUpdate).GetProperties()), user, transaction);
                        await DeleteUserModules(user.Id, conn, transaction);
                        await InsertUserModules(userRequest.Modules, user.Id, conn, transaction);
                        transaction.Commit();
                    }
                }
            }
            catch (Exception ex)
            {
                await _log.AddError($"Error en creacion de usuario - Create - Ex: {ex.Message} - Stack: {ex.StackTrace}");
                throw;
            }
        }

        private static async Task DeleteUserModules(int userId, IDbConnection conn, IDbTransaction transaction)
        {
            await conn.ExecuteAsync("DELETE FROM UsersModules WHERE UserId = @UserId", new { UserId = userId }, transaction);
        }

        private async Task InsertUserModules(List<ModuleRequest> userModules, int userId, IDbConnection conn, IDbTransaction transaction)
        {
            foreach (var item in userModules)
            {
                Module module = _mapper.Map<Module>(item);
                module.UserId = userId;
                module.CreatedDate = DateTime.Now;
                module.UserCreatedId = 1;
                module.ModifiedDate = DateTime.Now;
                module.UserModifiedId = 1;
                await conn.ExecuteAsync(GenerateInsertQuery("UsersModules", typeof(Module).GetProperties()), module, transaction);
            }
        }

        #region Private Methods

        private static async Task<IEnumerable<ModuleResponse>> GetUsersModulesByUserId(int userId, IDbConnection connection)
            => await connection.QueryAsync<ModuleResponse>("SELECT Id, Name FROM UsersModules WHERE UserId = @UserId", new { UserId = userId });

        #endregion
    }
}
