using ChatBoard.DataBase.Entity;
using ChatBoard.DataBase.Interface;
using ChatBoard.Services.Interface;

namespace ChatBoard.Services.Service
{
    public class MessageService(IUnitOfWork unitOfWork, IMessageRepository messageRepository) : IMessageService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMessageRepository _messageRepository = messageRepository;

        public async Task<IEnumerable<Message>> GetAllMessagesByGroup(int groupID)
        {
            var messages = await _messageRepository.GetMessagesByGroupIdAsync(groupID);
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
            await _messageRepository.AddAsync(newMessage);
            await _unitOfWork.SaveChangesAsync();

        }

        public async Task ClearMessages(int groupID)
        {
            var messages = await _messageRepository.GetMessagesByGroupIdAsync(groupID);
            if (messages != null)
            {
                foreach (var message in messages)
                {
                    _messageRepository.Remove(message);
                }
                await _unitOfWork.SaveChangesAsync();
            }
        }
    }
}
