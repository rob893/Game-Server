using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace GameServer.Hubs
{
    public class TestHub : Hub
    {
        public async Task NewMessage(string username, string message)
        {
            await Clients.All.SendAsync("messageReceived", username, message);
        }

        public async Task MovedPlayer(int x, int y)
        {
            await Clients.Others.SendAsync("playerMoved", x, y);
        }
    }
}