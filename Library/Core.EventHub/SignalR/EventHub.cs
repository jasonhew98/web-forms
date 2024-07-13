using Microsoft.AspNet.SignalR;
using System.Threading.Tasks;
using System.Web;

namespace EventHub.SignalR
{
    public class EventHub : Hub
    {
        public EventHub()
        {

        }

        public override async Task OnConnected()
        {
            var context = HttpContext.Current.User.Identity;
            await Clients.All.ReceiveMessage("Hello from server");
        }
    }
}
