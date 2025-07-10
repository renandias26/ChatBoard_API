using ChatBoard.DataBase.Entity;

namespace ChatBoard.DataBase.Interface
{
    public interface IMessageRepository : IBaseRepository<Message>
    {
        Task<IEnumerable<Message>> GetMessagesByGroupIdAsync(int groupId);
    }
}
