using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace ColegioMirim.Infrastructure.Data
{
    public class ColegioMirimContext : IDisposable
    {
        private readonly IConfiguration _configuration;
        public IDbConnection Connection { get; private set; }

        public ColegioMirimContext(IConfiguration configuration)
        {
            _configuration = configuration;
            Connection = BuildConnection();
        }

        private IDbConnection BuildConnection()
        {
            var connection = new SqlConnection(_configuration.GetConnectionString("Default"));
            connection.Open();

            return connection;
        }

        public void Dispose()
        {
            Connection?.Close();
            Connection?.Dispose();
        }
    }
}
