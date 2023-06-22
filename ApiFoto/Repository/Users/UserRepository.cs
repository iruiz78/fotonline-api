using ApiFoto.Domain.User;
using ApiFoto.Infrastructure.Dapper;
using ApiFoto.Repository.Generic;
using Dapper;

namespace ApiFoto.Repository.Users
{
    public class UserRepository : GenericRepository<User>
    {
        public UserRepository(DapperContext context) : base(context)
        {
            
        }

        public async new Task<IEnumerable<UserResponse>> GetAll()
        {
            using (var conn = _context.CreateConnectionSQL())
            {
                return await conn.QueryAsync<UserResponse>("SELECT * FROM Users WHERE Active = 1");
            }
        }

        public async Task<UserResponse> GetByUsername(string username)
        {
            using (var conn = _context.CreateConnectionSQL())
            {
                return await conn.QueryFirstOrDefaultAsync<UserResponse>("SELECT * FROM Users WHERE Username = @Username AND Active = 1", new { Username = username });
            }
        }
    }
}
