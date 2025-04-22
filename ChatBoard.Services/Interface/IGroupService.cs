using ChatBoard.DataBase.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatBoard.Services.Interface
{
    public interface IGroupService
    {
        Task<int> GetGroupByName(string GroupName);
        Task<Group> CreateGroup(string GroupName);
    }
}
