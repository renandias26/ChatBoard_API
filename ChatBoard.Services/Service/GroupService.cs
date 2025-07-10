using ChatBoard.DataBase.Entity;
using ChatBoard.DataBase.Interface;
using ChatBoard.Services.Interface;

namespace ChatBoard.Services.Service
{
    public class GroupService(IUnitOfWork unitOfWork, IGroupRepository groupRepository) : IGroupService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IGroupRepository _groupRepository = groupRepository;

        public async Task<int> GetGroupByName(string GroupName)
        {
            return await _groupRepository.GetGroupByName(GroupName); ;
        }

        public async Task<Group> CreateGroup(string GroupName)
        {
            var group = new Group
            {
                Name = GroupName
            };
            await _groupRepository.AddAsync(group);
            await _unitOfWork.SaveChangesAsync();
            return group;
        }
    }
}
