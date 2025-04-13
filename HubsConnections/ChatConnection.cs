using ChatBoard_API.Models;
using System.Collections.Concurrent;

namespace ChatBoard_API.HubsConnections
{
    public class ChatConnection
    {
        private readonly ConcurrentDictionary<string, UserConnection> _connections = new();

        public ConcurrentDictionary<string, UserConnection> connections => _connections;
    }
}
