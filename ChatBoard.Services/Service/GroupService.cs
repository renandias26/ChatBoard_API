using ChatBoard.DataBase.Entity;
using ChatBoard.DataBase.Interface;
using ChatBoard.Services.Interface;

namespace ChatBoard.Services.Service
{
    public class GroupService(IUnitOfWork unitOfWork) : IGroupService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<int> GetGroupByName(string GroupName)
        {
            return await _unitOfWork.Group.GetGroupByName(GroupName); ;
        }

        public async Task<Group> CreateGroup(string GroupName)
        {
            var group = new Group
            {
                Name = GroupName
            };
            await _unitOfWork.Group.AddAsync(group);
            await _unitOfWork.SaveChangesAsync();
            return group;
        }
    }
}
