using System.Collections.Concurrent;

namespace ChatBoard.Application.DTO.Hub
{
    public class ChatConnection
    {
        private readonly ConcurrentDictionary<string, UserConnection> _connections = new();
        public ConcurrentDictionary<string, UserConnection> connections => _connections;
    }
}
