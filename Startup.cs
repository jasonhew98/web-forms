using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Owin;
using System.Threading.Tasks;
using System.Web.Cors;

[assembly: OwinStartup(typeof(WebForms.Startup))]
namespace WebForms
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCors(new CorsOptions
            {
                PolicyProvider = new CorsPolicyProvider
                {
                    PolicyResolver = context => Task.FromResult(new CorsPolicy
                    {
                        AllowAnyOrigin = false,
                        Origins = { "http://localhost:8080" },
                        AllowAnyMethod = true,
                        AllowAnyHeader = true,
                        SupportsCredentials = true
                    })
                }
            });
            app.MapSignalR();
        }
    }
}
