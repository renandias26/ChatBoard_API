using ChatBoard.DataBase.Interface;

namespace ChatBoard.DataBase.Context
{
    public class UnitOfWork(DataBaseContext context) : IUnitOfWork
    {
        private readonly DataBaseContext _context = context;

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
