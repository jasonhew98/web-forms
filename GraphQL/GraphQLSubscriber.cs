using GraphQL;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;
using System;
using System.Reactive.Disposables;
using System.Threading.Tasks;
using WebForms.GraphQL.Subscriptions;

namespace WebForms.GraphQL
{
    public interface IGraphQlSubscriber
    {
        Task Subscribe<T>(GraphQLSubscriptionBase<T> subscriptionConfig);
        Task StopSubscriber();
    }

    public class GraphQLSubscriber : IGraphQlSubscriber
    {
        private static CompositeDisposable subscriptions;
        private static GraphQLHttpClient client;

        public GraphQLSubscriber()
        {
            subscriptions = new CompositeDisposable();

            var options = new GraphQLHttpClientOptions
            {
                EndPoint = new Uri("https://localhost:7286/graphql"),
                UseWebSocketForQueriesAndMutations = true,
            };
            client = new GraphQLHttpClient(options, new NewtonsoftJsonSerializer());
        }

        public Task Subscribe<T>(GraphQLSubscriptionBase<T> subscriptionConfig)
        {
            var messageReceiveRequest = new GraphQLRequest
            {
                Query = subscriptionConfig.Query
            };

            IObservable<GraphQLResponse<T>> subscriptionStream
                = client.CreateSubscriptionStream<T>(messageReceiveRequest);

            var subscription = subscriptionStream.Subscribe(response =>
            {
                subscriptionConfig.OnReceive(response);
            });

            subscriptions.Add(subscription);

            return Task.CompletedTask;
        }

        public Task StopSubscriber()
        {
            subscriptions?.Dispose();
            client?.Dispose();

            return Task.CompletedTask;
        }
    }
}