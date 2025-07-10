using ChatBoard.Application.DTO.Services.ChatService;
using ChatBoard.Application.Interface;
using ChatBoard.Domain.Interface.Services;

namespace ChatBoard.Application.Services
{
    public class ChatService(
        IGroupService GroupService,
        IMessageService MessageService
        ) : IChatService
    {
        private readonly IGroupService _groupService = GroupService;
        private readonly IMessageService _messageService = MessageService;

        public async Task<JoinChat> JoinChat(string RoomName)
        {
            var groupID = await _groupService.GetGroupByName(RoomName);

            if (groupID == 0)
            {
                var newGroup = await _groupService.CreateGroup(RoomName);
                groupID = newGroup.Id;
            }

            var groupMessages = await _messageService.GetAllMessagesByGroup(groupID);

            return new JoinChat { groupID = groupID, messages = groupMessages };
        }

        public async Task AddMessageToGroup(int groupID, string message, string UserName, DateTimeOffset dateTime)
        {
            await _messageService.AddMessageToGroup(groupID, message, UserName, dateTime);
        }

        public async Task ClearMessages(int groupID)
        {
            await _messageService.ClearMessages(groupID);
        }
    }
}
