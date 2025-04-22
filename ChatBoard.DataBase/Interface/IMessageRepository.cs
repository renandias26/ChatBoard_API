using ChatBoard.DataBase.Context;
using ChatBoard.DataBase.Entity;
using ChatBoard.DataBase.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatBoard.DataBase.Interface
{
    public interface IMessageRepository : IBaseRepository<Message>
    {
        Task<IEnumerable<Message>> GetMessagesByGroupIdAsync(int groupId);
    }
}
