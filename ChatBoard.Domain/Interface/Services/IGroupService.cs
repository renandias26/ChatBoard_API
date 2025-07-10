using ChatBoard.Domain.Entity;

namespace ChatBoard.Domain.Interface.Services
{
    public interface IGroupService
    {
        Task<int> GetGroupByName(string GroupName);
        Task<Group> CreateGroup(string GroupName);
    }
}
