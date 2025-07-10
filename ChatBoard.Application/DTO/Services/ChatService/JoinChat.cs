using ChatBoard.Domain.Entity;

namespace ChatBoard.Application.DTO.Services.ChatService
{
    public class JoinChat
    {
        public required IEnumerable<Message> messages;
        public required int groupID;
    }
}
