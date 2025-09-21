using Microsoft.AspNetCore.SignalR;
using ChatService.Entities;
using System.Collections.Concurrent;

namespace ChatService.Hubs;

public class ChatHub : Hub
{
    private static ConcurrentDictionary<string, string> ConnectedUsers = new();

    public async Task SendMessage(string user, string message)
    {
        var msg = new Message
        {
            Sender = user,
            Text = message,
            Timestamp = DateTime.UtcNow
        };

        await Clients.All.SendAsync("ReceiveMessage", msg);
    }

    public override async Task OnConnectedAsync()
    {
        var user = Context.GetHttpContext()?.Request.Query["user"].ToString() ?? "Unknown";

        ConnectedUsers[Context.ConnectionId] = user;

        await Clients.All.SendAsync("UpdateUserList", ConnectedUsers.Values.Distinct());

        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        ConnectedUsers.TryRemove(Context.ConnectionId, out _);

        await Clients.All.SendAsync("UpdateUserList", ConnectedUsers.Values.Distinct());

        await base.OnDisconnectedAsync(exception);
    }
}
