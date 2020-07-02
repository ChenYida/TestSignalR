using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace ServerSignalR
{
    public class ChatHub : Hub
    {
        public async Task Chat(string message)
        {
            await Clients.All.SendAsync("Chat", message);
        }
    }
}