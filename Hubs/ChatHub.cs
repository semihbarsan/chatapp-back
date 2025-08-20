using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace ChatApp.Hubs; // veya projenin ana namespace'i

public class ChatHub : Hub

{


    // Mesajı belirli bir odaya gönderir
    public async Task SendMessageToRoom(string user, string message, string roomName)

    {
        await Clients.Group(roomName).SendAsync("ReceiveMessage", user, message);
    }
    public async Task JoinChat(string user, string roomName)

    {
        // Kullanıcıyı odaya ekler
        await Groups.AddToGroupAsync(Context.ConnectionId, roomName);
    }

    public async Task LeaveChat(string user, string roomName)

    {
        // Kullanıcıyı odadan çıkarır
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, roomName);
    }

}