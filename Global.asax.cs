using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Http;
using WebForms.GraphQL;
using Microsoft.Extensions.DependencyInjection;
using WebForms.GraphQL.Subscriptions;
using WebForms.Services.Interface;
using WebForms.Services;

namespace WebForms
{
    public class Global : HttpApplication
    {
        private static IServiceProvider _serviceProvider;
        private static IGraphQLService _graphQLService;

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

            _graphQLService = _serviceProvider.GetService<IGraphQLService>();

            ConfigureGraphQLSubscriptions();
        }

        void Application_End(object sender, EventArgs e)
        {
            if (_graphQLService != null)
            {
                _graphQLService.StopSubscriber();
            }
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IGraphQLService, GraphQLService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<MessageSubscriptionHandler>();
        }

        private void ConfigureGraphQLSubscriptions()
        {
            _graphQLService.Subscribe<MessageSubscription, MessageSubscriptionResponse, MessageSubscriptionHandler>();
        }
    }
}