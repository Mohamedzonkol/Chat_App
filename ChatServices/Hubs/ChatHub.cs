using ChatServices.DataServices;
using ChatServices.Models;
using Microsoft.AspNetCore.SignalR;

namespace ChatServices.Hubs
{
    public class ChatHub(MemoryDb db) : Hub
    {
        private readonly MemoryDb _db = db;

        public async Task joinChat(UserConnection user)
        {
            await Clients.All.SendAsync("ReceiveMessage", "Admin", $"{user.UserName} Has Join");
        }

        public async Task joinSpecificChatRoom(UserConnection conn)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, conn.ChatRoom);
            _db.connections[Context.ConnectionId]=conn;
            await Clients.Group(conn.ChatRoom).SendAsync("ReceiveMessage",
                "Admin",$"{conn.UserName} Has Succesfilly Join to {conn.ChatRoom} Chat Room");
        }

        public async Task SendMessage(string message)
        {
            if (_db.connections.TryGetValue(Context.ConnectionId, out UserConnection conn))
            {
                await Clients.Group(conn.ChatRoom).SendAsync("ReceiveSpecificMessage",conn.UserName,message);
            }
        }
    }
}
