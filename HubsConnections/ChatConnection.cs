using Chat.Models;
using System.Collections.Concurrent;

namespace Chat.HubsConnections
{
    public class ChatConnection
    {
        private readonly ConcurrentDictionary<string, UserConnection> _connections = new();

        public ConcurrentDictionary<string, UserConnection> connections => _connections;
    }
}
