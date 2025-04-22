using ChatBoard.DataBase.Context;
using ChatBoard.DataBase.Entity;
using ChatBoard.DataBase.Interface;
using Microsoft.EntityFrameworkCore;

namespace ChatBoard.DataBase.Repository
{
    public class MessageRepository(DataBaseContext context) : IMessageRepository
    {
        private readonly BaseRepository<Message> _baseRepository = new(context);
        private readonly DataBaseContext _context = context;

        public async Task AddAsync(Message entity)
        {
            await _baseRepository.AddAsync(entity);
        }

        public void Remove(Message entity)
        {
            _baseRepository.Remove(entity);
        }

        public async Task<IEnumerable<Message>> GetAllAsync()
        {
            return await _baseRepository.GetAllAsync();
        }

        public async Task<Message?> GetByIdAsync(int id)
        {
            return await _baseRepository.GetByIdAsync(id);
        }

        public void Update(Message entity)
        {
            _baseRepository.Update(entity);
        }

        public async Task<IEnumerable<Message>> GetMessagesByGroupIdAsync(int groupId)
        {
            return await _context.Message
                .Where(m => m.GroupId == groupId)
                .OrderByDescending(m => m.DateTime)
                .ToListAsync();
        }
    }
}
