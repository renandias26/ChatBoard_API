using ChatBoard.Domain.Entity;
using ChatBoard.Domain.Interface.Repository;
using ChatBoard.Infrastructure.DataBase.Context;
using Microsoft.EntityFrameworkCore;

namespace ChatBoard.Infrastructure.DataBase.Repository
{
    public class GroupRepository(DataBaseContext context) : BaseRepository<Group>(context), IGroupRepository
    {
        public async Task<int> GetGroupByName(string GroupName)
        {
            return await _dbSet
                .Where(p => p.Name == GroupName)
                .Select(p => p.Id)
                .FirstOrDefaultAsync();
        }
    }
}
