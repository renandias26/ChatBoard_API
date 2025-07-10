using ChatBoard.Domain.Entity;

namespace ChatBoard.Domain.Interface.Repository
{
    public interface IMessageRepository : IBaseRepository<Message>
    {
        Task<IEnumerable<Message>> GetMessagesByGroupIdAsync(int groupId);
    }
}
