using ColegioMirim.Core.Data;
using System.Transactions;

namespace ColegioMirim.Infrastructure.Data
{
    public class UnityOfWork : IUnityOfWork
    {
        private TransactionScope _transaction;

        public void BeginTransaction()
        {
            if (!HasTransaction())
                _transaction = new TransactionScope(
                    TransactionScopeOption.Required,
                    new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted },
                    TransactionScopeAsyncFlowOption.Enabled
                );
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
