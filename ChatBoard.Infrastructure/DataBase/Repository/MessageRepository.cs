using ChatBoard.Domain.Entity;
using ChatBoard.Domain.Interface.Repository;
using ChatBoard.Infrastructure.DataBase.Context;
using Microsoft.EntityFrameworkCore;

namespace ChatBoard.Infrastructure.DataBase.Repository
{
    public class MessageRepository(DataBaseContext context) : BaseRepository<Message>(context), IMessageRepository
    {
        public async Task<IEnumerable<Message>> GetMessagesByGroupIdAsync(int groupId)
        {
            return await _dbSet
                .Where(m => m.GroupId == groupId)
                .OrderByDescending(m => m.DateTime)
                .ToListAsync();
        }
    }
}
