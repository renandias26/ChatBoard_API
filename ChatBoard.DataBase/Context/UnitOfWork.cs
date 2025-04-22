using ChatBoard.DataBase.Interface;
using Microsoft.EntityFrameworkCore.Storage;

namespace ChatBoard.DataBase.Context
{
    public class UnitOfWork(
        DataBaseContext context,
        IMessageRepository messageRepository,
        IGroupRepository groupRepository
        ) : IUnitOfWork
    {
        private readonly DataBaseContext _context = context;
        private IDbContextTransaction? _transaction;

        public IMessageRepository Message { get; } = messageRepository;
        public IGroupRepository Group { get; } = groupRepository;

        public void BeginTransaction()
        {
            _transaction = _context.Database.BeginTransaction();
        }

        public async Task CommitAsyncTransaction()
        {
            try
            {
                if (_transaction == null) { return; }
                await _transaction.CommitAsync();
            }
            finally
            {
                this.Dispose();
                _transaction = null;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _transaction?.Dispose();
                _transaction = null;
                _context.Dispose();
            }
        }

        public void RollbackTransaction()
        {
            try
            {
                _transaction?.Rollback();
            }
            finally
            {
                this.Dispose();
            }
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
