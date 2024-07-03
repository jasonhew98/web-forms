using Microsoft.Extensions.DependencyInjection;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Owin;
using System;
using System.Threading.Tasks;
using System.Web.Cors;
using WebForms.GraphQL;

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
                        AllowAnyOrigin = false, // Allow specific origins only
                        Origins = { "http://localhost:8080" }, // Add your allowed origins here
                        AllowAnyMethod = true,
                        AllowAnyHeader = true,
                        SupportsCredentials = true // Allow credentials
                    })
                }
            });
            app.MapSignalR();
        }
    }
}
