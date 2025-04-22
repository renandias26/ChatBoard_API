using ChatBoard.DTO.Services.ChatService;

namespace ChatBoard.Services.Interface
{
    public interface IChatService
    {
        Task<JoinChat> JoinChat(string RoomName);
        Task AddMessageToGroup(int groupID, string message, string UserName, DateTimeOffset dateTime);
        Task ClearMessages(int groupID);
    }
}
