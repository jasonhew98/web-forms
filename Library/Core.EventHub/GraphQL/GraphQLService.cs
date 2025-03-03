﻿using Core.EventHub.GraphQL.Interfaces;
using GraphQL;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reactive.Disposables;
using System.Threading.Tasks;

namespace Core.EventHub.GraphQL
{
    public interface IGraphQLService
    {
        void Subscribe<TSubscription, TSubscriptionResponse, TSubscriptionHandler>()
            where TSubscription : IRequest, new()
            where TSubscriptionResponse : IResponse
            where TSubscriptionHandler : ISubscriptionHandler<TSubscription, TSubscriptionResponse>;
        Task StopSubscriber();
    }

    public class GraphQLService : IGraphQLService
    {
        private static CompositeDisposable subscriptions;
        private static GraphQLHttpClient client;

        private readonly IServiceProvider _serviceProvider;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public GraphQLService(
            IServiceProvider serviceProvider,
            IServiceScopeFactory serviceScopeFactory)
        {
            _serviceProvider = serviceProvider;
            _serviceScopeFactory = serviceScopeFactory;

            subscriptions = new CompositeDisposable();

            var options = new GraphQLHttpClientOptions
            {
                EndPoint = new Uri("https://localhost:7286/graphql"),
                UseWebSocketForQueriesAndMutations = true,
            };
            client = new GraphQLHttpClient(options, new NewtonsoftJsonSerializer());
        }

        public void Subscribe<TSubscription, TSubscriptionResponse, TSubscriptionHandler>()
            where TSubscription : IRequest, new()
            where TSubscriptionResponse : IResponse
            where TSubscriptionHandler : ISubscriptionHandler<TSubscription, TSubscriptionResponse>
        {
            var subscriptionInstance = new TSubscription();
            var query = subscriptionInstance.Query;

            var messageReceiveRequest = new GraphQLRequest { Query = query };

            var subscriptionStream = client.CreateSubscriptionStream<TSubscriptionResponse>(messageReceiveRequest);
            var subscription = subscriptionStream.Subscribe(async response =>
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var handler = scope.ServiceProvider.GetRequiredService<TSubscriptionHandler>();
                    await handler.HandleAsync(response);
                }
            });

            subscriptions.Add(subscription);
        }

        public Task StopSubscriber()
        {
            subscriptions.Dispose();
            client.Dispose();

            return Task.CompletedTask;
        }
    }
}