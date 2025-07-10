using ChatBoard.DataBase.Context;
using ChatBoard.DataBase.Entity;
using ChatBoard.DataBase.Interface;
using Microsoft.EntityFrameworkCore;

namespace ChatBoard.DataBase.Repository
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
