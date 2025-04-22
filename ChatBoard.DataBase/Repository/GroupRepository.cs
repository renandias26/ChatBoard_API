using ChatBoard.DataBase.Context;
using ChatBoard.DataBase.Entity;
using ChatBoard.DataBase.Interface;
using Microsoft.EntityFrameworkCore;

namespace ChatBoard.DataBase.Repository
{
    public class GroupRepository(DataBaseContext context) : IGroupRepository
    {
        private readonly BaseRepository<Group> _baseRepository = new(context);
        private readonly DataBaseContext _context = context;

        public async Task AddAsync(Group entity)
        {
            await _baseRepository.AddAsync(entity);
        }

        public async Task<IEnumerable<Group>> GetAllAsync()
        {
            return await _baseRepository.GetAllAsync();
        }

        public async Task<Group?> GetByIdAsync(int id)
        {
            return await _baseRepository.GetByIdAsync(id);
        }

        public void Remove(Group entity)
        {
            _baseRepository.Remove(entity);
        }

        public void Update(Group entity)
        {
            _baseRepository.Update(entity);
        }

        public async Task<int> GetGroupByName(string GroupName)
        {
            return await _context.Group.Where(p => p.Name == GroupName).Select(p => p.Id).FirstOrDefaultAsync();
        }
    }
}
