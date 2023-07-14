using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace ColegioMirim.Infrastructure.Data
{
    public class ColegioMirimContext
    {
        private readonly IConfiguration _configuration;

        public ColegioMirimContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IDbConnection BuildConnection()
        {
            return new SqlConnection(_configuration.GetConnectionString("Default"));
        }
    }
}
