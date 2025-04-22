using ChatBoard.API.HubsConnections;
using ChatBoard.DTO.Hub;
using ChatBoard.DTO.Request;
using ChatBoard.Services.Interface;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;

namespace ChatBoard.API.Hubs
{
    public class ChatHub(
        ChatConnection connection,
        IChatService ChatService
        ) : Hub
    {
        private readonly ConcurrentDictionary<string, UserConnection> _connections = connection.connections;
        private readonly IChatService _chatService = ChatService;

        public async Task JoinSpecificChat(UserConnectionDTO conn)
        {
            var groupData = await _chatService.JoinChat(conn.ChatRoom);

            if (groupData == null) { return; }

            await Groups.AddToGroupAsync(Context.ConnectionId, conn.ChatRoom);

            _connections[Context.ConnectionId] = new UserConnection { GroupID = groupData.groupID, UserName = conn.UserName, GroupName = conn.ChatRoom };

            var Response = groupData.messages.Select(m => new { m.UserName, m.Content, m.DateTime });

            await Clients.Caller.SendAsync("StartChat", Response);

            await Clients.OthersInGroup(conn.ChatRoom).SendAsync("JoinChat", conn.UserName);
        }


        public async Task SendMessage(string message)
        {
            if (_connections.TryGetValue(Context.ConnectionId, out UserConnection? conn))
            {
                //DataTime da mensagem virá do FRONT?
                DateTimeOffset dateTime = DateTimeOffset.UtcNow;
                await _chatService.AddMessageToGroup(conn.GroupID, message, conn.UserName, dateTime);
                var Response = new { conn.UserName, content = message, dateTime };
                await Clients.OthersInGroup(conn.GroupName).SendAsync("ReceiveMessage", Response);
            }
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            if (_connections.TryRemove(Context.ConnectionId, out UserConnection? conn))
            {
                await Clients.OthersInGroup(conn.GroupName).SendAsync("DisconnectedUser", conn.UserName);
            }
            await base.OnDisconnectedAsync(exception);
        }

        public async Task ClearMessages()
        {
            if (_connections.TryGetValue(Context.ConnectionId, out UserConnection? conn))
            {
                await _chatService.ClearMessages(conn.GroupID);
                await Clients.OthersInGroup(conn.GroupName).SendAsync("ClearMessages");
            }
        }
    }
}
