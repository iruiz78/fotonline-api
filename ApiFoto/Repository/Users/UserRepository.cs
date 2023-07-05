using ApiFoto.Domain.User;
using ApiFoto.Infrastructure.Dapper;
using ApiFoto.Repository.Generic;
using AutoMapper;
using Dapper;

namespace ApiFoto.Repository.Users
{
    public class UserRepository : GenericRepository<User>
    {
        private readonly IMapper _mapper;
        public UserRepository(DapperContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public async new Task<IEnumerable<UserResponse>> GetAll()
        {
            using (var conn = _context.CreateConnectionSQL())
            {
                return await conn.QueryAsync<UserResponse>("SELECT * FROM Users WHERE Active = 1");
            }
        }

        public async Task<UserResponse> GetByMail(string mail)
        {
            using (var conn = _context.CreateConnectionSQL())
            {
                return await conn.QueryFirstOrDefaultAsync<UserResponse>("SELECT * FROM Users WHERE Mail = @Mail AND Active = 1", new { Mail = mail });
            }
        }

        public async Task<int> Create(UserRequest userRequest)
        {
            try
            {
                var user = _mapper.Map<User>(userRequest);
                user.CreatedDate = DateTime.Now;
                user.UserCreatedId = 9;
                user.ModifiedDate = DateTime.Now;
                user.UserModifiedId = 9;

                using (var conn = _context.CreateConnectionSQL())
                {
                    return await conn.ExecuteAsync(GenerateInsertQuery(), user);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
