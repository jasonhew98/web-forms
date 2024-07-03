using Microsoft.AspNet.SignalR;
using System.Threading.Tasks;

namespace WebForms.EventHub
{
    public class WebHub : Hub
    {
        public override async Task OnConnected()
        {
            await Clients.All.ReceiveMessage("Hello from server");
        }
    }
}