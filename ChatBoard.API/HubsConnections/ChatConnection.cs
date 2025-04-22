using ChatBoard.DTO.Hub;
using System.Collections.Concurrent;

namespace ChatBoard.API.HubsConnections
{
    public class ChatConnection
    {
        private readonly ConcurrentDictionary<string, UserConnection> _connections = new();
        public ConcurrentDictionary<string, UserConnection> connections => _connections;
    }
}
