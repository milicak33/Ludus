using Microsoft.AspNetCore.SignalR;
using ChatService.Entities;

namespace ChatService.Hubs;
public class ChatHub : Hub
{
    public async Task SendMessage(string user, string message)
    {
        var msg = new Message { Sender = user, Text = message, Timestamp = DateTime.UtcNow };
        await Clients.All.SendAsync("ReceiveMessage", msg);
    }
}