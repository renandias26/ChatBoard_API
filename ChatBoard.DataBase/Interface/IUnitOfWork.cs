namespace ChatBoard.DataBase.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        IMessageRepository Message { get; }
        IGroupRepository Group { get; }

        Task<int> SaveChangesAsync();
        void BeginTransaction();
        Task CommitAsyncTransaction();
        void RollbackTransaction();
    }
}
