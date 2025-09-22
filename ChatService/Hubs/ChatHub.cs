using Microsoft.AspNetCore.SignalR;
using ChatService.Entities;

namespace ChatService.Hubs;

public class ChatHub : Hub
{
    private static readonly Dictionary<string, HashSet<string>> OnlineUsers = new();

    public override async Task OnConnectedAsync()
    {
        var user = Context.GetHttpContext()?.Request.Query["user"].ToString();
        if (!string.IsNullOrEmpty(user))
        {
            if (!OnlineUsers.ContainsKey(user))
                OnlineUsers[user] = new HashSet<string>();

            OnlineUsers[user].Add(Context.ConnectionId);

            await Clients.All.SendAsync("UpdateUserList", OnlineUsers.Keys.ToList());
        }
        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        var userEntry = OnlineUsers.FirstOrDefault(x => x.Value.Contains(Context.ConnectionId));

        if (!string.IsNullOrEmpty(userEntry.Key))
        {
            userEntry.Value.Remove(Context.ConnectionId);

            if (userEntry.Value.Count == 0)
                OnlineUsers.Remove(userEntry.Key);

            await Clients.All.SendAsync("UpdateUserList", OnlineUsers.Keys.ToList());
        }
        await base.OnDisconnectedAsync(exception);
    }

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
}
