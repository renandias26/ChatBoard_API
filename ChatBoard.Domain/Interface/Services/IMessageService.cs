using ChatBoard.Domain.Entity;

namespace ChatBoard.Domain.Interface.Services
{
    public interface IMessageService
    {
        Task AddMessageToGroup(int groupID, string message, string UserName, DateTimeOffset dateTime);
        Task ClearMessages(int groupID);
        Task<IEnumerable<Message>> GetAllMessagesByGroup(int groupID);
    }
}
