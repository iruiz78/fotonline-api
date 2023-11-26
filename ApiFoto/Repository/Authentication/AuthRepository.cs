using ApiFoto.Infrastructure.Auth.Domain;
using ApiFoto.Infrastructure.Dapper;
using ApiFoto.Repository.Generic;
using Dapper;
using Loggin;
using Microsoft.Extensions.Options;

namespace ApiFoto.Repository.Authentication
{
    public class AuthRepository : GenericRepository<Auth>
    {
        public AuthRepository(DapperContext context, IOptions<Log> log) : base(context, log)
        {
            
        }

        public async Task SaveRecoveryCode(RecoveryCode recoveryCode)
        {
            recoveryCode.CreatedDate = DateTime.Now;
            recoveryCode.UserCreatedId = 9;
            recoveryCode.ModifiedDate = DateTime.Now;
            recoveryCode.UserModifiedId = 9;

            using var conn = _context.CreateConnectionSQL();

            try
            {
                await conn.ExecuteAsync(GenerateInsertQuery("RecoveryCode", typeof(RecoveryCode).GetProperties()), recoveryCode);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<RecoveryCode>> GetRecoveryCodeByMail(string email)
        {
            using var conn = _context.CreateConnectionSQL();

            try
            {
                return await conn.QueryAsync<RecoveryCode>("SELECT TOP 5 * FROM RecoveryCode " +
                                                           "WHERE Email = @Email " +
                                                           "ORDER BY CreatedDate DESC", new { Email = email }); 
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task ResetPassword(int id, string password)
        {
            using var conn = _context.CreateConnectionSQL();

            try
            {
                await conn.QueryAsync("UPDATE Users SET Password = @Password WHERE Id = @Id", new { Id = id, Password = password });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task SaveRefreshToken(RefreshToken token)
        {
            try
            {
                token.CreatedDate = DateTime.Now;
                token.UserCreatedId = 1;
                token.ModifiedDate = DateTime.Now;
                token.UserModifiedId = 1;
                using (var conn = _context.CreateConnectionSQL())
                {
                    await conn.ExecuteAsync(GenerateInsertQuery("RefreshTokens", typeof(RefreshToken).GetProperties()), token);
                }
            }
            catch (Exception ex)
            {

                throw;
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
