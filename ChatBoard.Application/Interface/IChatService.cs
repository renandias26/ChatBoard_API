using ChatBoard.Application.DTO.Services.ChatService;

namespace ChatBoard.Application.Interface
{
    public interface IChatService
    {
        Task<JoinChat> JoinChat(string RoomName);
        Task AddMessageToGroup(int groupID, string message, string UserName, DateTimeOffset dateTime);
        Task ClearMessages(int groupID);
    }
}
