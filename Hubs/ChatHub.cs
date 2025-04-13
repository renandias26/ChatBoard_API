using ChatBoard_API.HubsConnections;
using ChatBoard_API.Models;
using Microsoft.AspNetCore.SignalR;

namespace ChatBoard_API.Hubs
{
    public class ChatHub(ChatConnection connection) : Hub
    {
        private readonly ChatConnection _connection = connection;

        public async Task JoinChat(UserConnection conn)
        {
            await Clients.All.SendAsync("ReceiveMessage", "admin", $"{conn.UserName} has joined");
        }

        public async Task JoinSpecificChat(UserConnection conn)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, conn.ChatRoom);

            _connection.connections[Context.ConnectionId] = conn;

            //GetAllMessagesByGroupId(conn.ChatRoom);

            await Clients.Group(conn.ChatRoom).SendAsync("ReceiveMessage", "admin", $"{conn.UserName} has joined {conn.ChatRoom}");
        }

        public async Task SendMessage(string message)
        {
            if (_connection.connections.TryGetValue(Context.ConnectionId, out UserConnection? conn))
            {
                // AddMessageToGroup(conn.ChatRoom, message);
                await Clients.GroupExcept(conn.ChatRoom, Context.ConnectionId).SendAsync("ReceiveMessage", conn.UserName, message);
            }
        } 
    }
}
