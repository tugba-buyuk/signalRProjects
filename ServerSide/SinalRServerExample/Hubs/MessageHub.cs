using Microsoft.AspNetCore.SignalR;

namespace SinalRServerExample.Hubs
{
    public class MessageHub :Hub
    {
        public async Task SendMessageAsync(string message,IEnumerable<string> connectionIds,string groupName, IEnumerable<string> groups)
        {
            #region Notlar
            //Caller:
            await Clients.Caller.SendAsync("receiveMessage", message); // Server'a kim istekte bulunuyorsa sadec3e ona mesaj gider.

            //Other:
            await Clients.Others.SendAsync("receiveMessage", message); // İstek atan dışında herkese mesaj gider.

            //All:
            await Clients.All.SendAsync("receiveMessage", message); // Herkese mesaj gider.

            //AllExpect Metodu:
            await Clients.AllExcept(connectionIds).SendAsync("receiveMessage", message); //AllExpect metodu ile verilen connection Id'li client'lar dışında herkese  mesaj gider.

            //Client Metodu:
            await Clients.Client(connectionIds.First()).SendAsync("reveiveMessage", message); // Client methodunun içine hangi connection Id yazılırsa sadece o client'a mesaj gider.

            //Clients Methodu:
            await Clients.Clients(connectionIds).SendAsync("receiveMessage",message); // Sadece belirtilen client'lara mesaj yollar.

            //Group Metodu:
            //Önce grubu oluştur daha sonra clientları gruba subscribe et en son da o gruba mesaj yolla.
            await Clients.Group(groupName).SendAsync("receiveMesage", message); // girilen gruba mesaj yollanır.

            //GroupExcept Metodu:
            // Belirtilen gruptaki belirtilen client/clientlar dışındaki herkese mesaj gider
            await Clients.GroupExcept(groupName, connectionIds).SendAsync("receiveMesssage", message);

            //Groups Metodu:
            //IEnumerable şeklinde grupları veriyoruz.Grup listesindeki tüm clientlara mesaj gidiyor.
            await Clients.Groups(groups).SendAsync("receiveMessage", message);

            //OthesInGroup Metodu:
            //İstekte bulunan client dışında gruptaki diğer clientlara mesaj gider.
            await Clients.OthersInGroup(groupName).SendAsync("reiveMessage", message);

            #endregion
        }
        public override async Task OnConnectedAsync()
        {
            await Clients.Caller.SendAsync("getConnection",Context.ConnectionId);
        }


        // gruba ekşleme işlemi yapılıyor.
        public async Task addGroup(string connectionId,string groupName)
        {
            await Groups.AddToGroupAsync(connectionId, groupName);
        }
    }
}
