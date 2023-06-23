using ApiFoto.Infrastructure.Auth.Domain;
using ApiFoto.Infrastructure.Dapper;
using ApiFoto.Repository.Generic;
using Dapper;

namespace ApiFoto.Repository.Authentication
{
    public class AuthRepository : GenericRepository<Auth>
    {
        public AuthRepository(DapperContext context) : base(context)
        {
            
        }

        public async Task SaveRefreshToken(RefreshToken token)
        {
            token.CreatedDate = DateTime.Now;
            token.UserCreatedId = 9;
            token.ModifiedDate = DateTime.Now;
            token.UserModifiedId = 9;
            using (var conn = _context.CreateConnectionSQL())
            {
                await conn.ExecuteAsync(GenerateInsertQuery("RefreshTokens", typeof(RefreshToken).GetProperties()), token);
            }
        }

        public async Task<RefreshTokenResponse> GetRefreshTokenByUserId(int userId, string token, string refreshToken)
        {
            using (var conn = _context.CreateConnectionSQL())
            {
                return await conn.QueryFirstOrDefaultAsync<RefreshTokenResponse>("SELECT TOP 1 * FROM RefreshTokens WHERE UserId = @UserId AND Token = @Token AND TokenRefresh = @RefreshToken ORDER BY CreatedDate DESC", new { UserId = userId , Token = token ,RefreshToken = refreshToken });
            }
        }

    }
}
