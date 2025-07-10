using ChatBoard.Domain.Entity;

namespace ChatBoard.Domain.Interface.Repository
{
    public interface IGroupRepository : IBaseRepository<Group>
    {
        Task<int> GetGroupByName(string GroupName);
    }
}
