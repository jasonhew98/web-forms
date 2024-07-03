using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Http;
using WebForms.GraphQL;
using Microsoft.Extensions.DependencyInjection;
using WebForms.GraphQL.Subscriptions;

namespace WebForms
{
    public class Global : HttpApplication
    {
        private static IServiceProvider _serviceProvider;
        private static IGraphQlSubscriber _graphQlSubscriber;

        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            _serviceProvider = serviceCollection.BuildServiceProvider();

            _graphQlSubscriber = _serviceProvider.GetService<IGraphQlSubscriber>();

            ConfigureGraphQLSubscriptions();
        }

        void Application_End(object sender, EventArgs e)
        {
            if (_graphQlSubscriber != null)
            {
                _graphQlSubscriber.StopSubscriber();
            }
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IGraphQlSubscriber, GraphQLSubscriber>();
        }

        private void ConfigureGraphQLSubscriptions()
        {
            _graphQlSubscriber.Subscribe(new MessageSubscription());
        }
    }
}