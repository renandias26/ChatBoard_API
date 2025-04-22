using ChatBoard.DataBase.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatBoard.Services.Interface
{
    public interface IMessageService
    {
        Task AddMessageToGroup(int groupID, string message, string UserName, DateTimeOffset dateTime);
        Task ClearMessages(int groupID);
        Task<IEnumerable<Message>> GetAllMessagesByGroup(int groupID);
    }
}
