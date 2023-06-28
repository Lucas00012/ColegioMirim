namespace ColegioMirim.Core.Data
{
    public interface IUnityOfWork
    {
        bool HasTransaction();
        void BeginTransaction();
        void CommitTransaction();
        void ClearTransaction();
    }
}
