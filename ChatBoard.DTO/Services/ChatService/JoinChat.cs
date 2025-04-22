using ChatBoard.DataBase.Entity;

namespace ChatBoard.DTO.Services.ChatService
{
    public class JoinChat
    {
        public required IEnumerable<Message> messages;
        public required int groupID;
    }
}
