using ChatBoard.DataBase.Context;
using ChatBoard.DataBase.Entity;
using ChatBoard.DataBase.Interface;
using Microsoft.EntityFrameworkCore;

namespace ChatBoard.DataBase.Repository
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
