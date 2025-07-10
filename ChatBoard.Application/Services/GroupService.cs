using ChatBoard.Domain.Entity;
using ChatBoard.Domain.Interface.Repository;
using ChatBoard.Domain.Interface.Services;

namespace ChatBoard.Application.Services
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
