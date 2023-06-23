using System.Data;
using System.Data.SqlClient;

namespace ApiFoto.Infrastructure.Dapper
{
    public class DapperContext
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;
        public DapperContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("FotoDbConnection");
        }

        public IDbConnection CreateConnectionSQL()
            => new SqlConnection(_connectionString);

    }
}
