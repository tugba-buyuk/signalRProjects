using Microsoft.AspNetCore.SignalR;
using SinalRServerExample.Interfaces;

namespace SinalRServerExample.Hubs
{
    public class MyHub :Hub<IMessageClient>
    {
        static List<string> clients = new List<string>();
        //public async Task SendMessageAsync(string message)
        //{
        //    await Clients.All.SendAsync("receiveMessage",message);
        //}
        public override async Task OnConnectedAsync()
        {
            clients.Add(Context.ConnectionId);
            await Clients.All.Clients(clients);
            await Clients.All.UserJoined(Context.ConnectionId);
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            clients.Remove(Context.ConnectionId);
            await Clients.All.Clients(clients);
            await Clients.All.UserLeft(Context.ConnectionId);
        }
    }
}
