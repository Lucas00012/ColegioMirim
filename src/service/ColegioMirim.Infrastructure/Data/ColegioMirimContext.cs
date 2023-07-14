using ColegioMirim.Core.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Transactions;

namespace ColegioMirim.Infrastructure.Data
{
    public class ColegioMirimContext : IUnityOfWork, IDisposable
    {
        private readonly IConfiguration _configuration;
        private readonly HttpContext _httpContext;
        private TransactionScope _transaction;

        public IDbConnection Connection { get; private set; }

        public ColegioMirimContext(IConfiguration configuration, IHttpContextAccessor httpContext)
        {
            _configuration = configuration;
            _httpContext = httpContext.HttpContext;

            Connection = BuildConnection();
        }

        private IDbConnection BuildConnection()
        {
            BeginTransaction();
            var connection = new SqlConnection(_configuration.GetConnectionString("Default"));
            connection.Open();

            return connection;
        }

        public void Dispose()
        {
            Connection?.Close();
            Connection?.Dispose();
        }

        public void BeginTransaction()
        {
            if (_httpContext.Request.Method != "GET" && !HasTransaction())
            {
                _transaction = new TransactionScope(
                    TransactionScopeOption.Required,
                    new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted },
                    TransactionScopeAsyncFlowOption.Enabled);
            }
        }

        public void CommitTransaction()
        {
            try
            {
                _transaction?.Complete();
            }
            finally
            {
                ClearTransaction();
            }
        }

        public bool HasTransaction()
        {
            return _transaction is not null;
        }

        public void ClearTransaction()
        {
            if (HasTransaction())
            {
                _transaction.Dispose();
                _transaction = null;
            }
        }
    }
}
