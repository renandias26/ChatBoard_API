using ChatBoard.DataBase.Entity;
using ChatBoard.DataBase.Interface;
using ChatBoard.Services.Interface;

namespace ChatBoard.Services.Service
{
    public class MessageService(IUnitOfWork unitOfWork) : IMessageService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        public async Task<IEnumerable<Message>> GetAllMessagesByGroup(int groupID)
        {
            var messages = await _unitOfWork.Message.GetMessagesByGroupIdAsync(groupID);
            return messages;
        }

        public async Task AddMessageToGroup(int groupID, string message, string UserName, DateTimeOffset dateTime)
        {
            var newMessage = new Message
            {
                GroupId = groupID,
                Content = message,
                UserName = UserName,
                DateTime = dateTime,
            };
            await _unitOfWork.Message.AddAsync(newMessage);
            await _unitOfWork.SaveChangesAsync();

        }

        public async Task ClearMessages(int groupID)
        {
            var messages = await _unitOfWork.Message.GetMessagesByGroupIdAsync(groupID);
            if (messages != null)
            {
                foreach (var message in messages)
                {
                    _unitOfWork.Message.Remove(message);
                }
                await _unitOfWork.SaveChangesAsync();
            }
        }
    }
}
