using ChatBoard.DataBase.Entity;

namespace ChatBoard.DataBase.Interface
{
    public interface IGroupRepository : IBaseRepository<Group>
    {
        Task<int> GetGroupByName(string GroupName);
    }
}
