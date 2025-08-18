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



    // Kullanıcıyı bir odaya ekler

    public async Task JoinChat(string user, string roomName)

    {

        // Kullanıcıyı odaya ekler

        await Groups.AddToGroupAsync(Context.ConnectionId, roomName);



        // Odaya katıldığını tüm kullanıcılara bildirir (sistem mesajı)

        await Clients.Group(roomName).SendAsync("ReceiveMessage", "Sistem", $"{user} sohbete katıldı.");

    }



    // Kullanıcıyı bir odadan çıkarır

    public async Task LeaveChat(string user, string roomName)

    {

        // Kullanıcıyı odadan çıkarır

        await Groups.RemoveFromGroupAsync(Context.ConnectionId, roomName);



        // Odadan ayrıldığını tüm kullanıcılara bildirir

        await Clients.Group(roomName).SendAsync("ReceiveMessage", "Sistem", $"{user} sohbetten ayrıldı.");

    }

}