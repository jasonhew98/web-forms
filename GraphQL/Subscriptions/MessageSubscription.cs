using GraphQL;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using WebForms.GraphQL.Interface;
using WebForms.Services.Interface;

namespace WebForms.GraphQL.Subscriptions
{
    public class MessageSubscriptionResponse : ISubscriptionResponse
    {
        public MessageReceive OnMessageReceived { get; set; }

        public class MessageReceive
        {
            public string Text { get; set; }
        }
    }

    public class MessageSubscription : Subscription
    {
        public override string Query => @"
            subscription {
                onMessageReceived {
                    text
                }
            }";
    }

    public class MessageSubscriptionHandler : ISubscriptionHandler<MessageSubscription, MessageSubscriptionResponse>
    {
        private readonly IProductService _productService;

        public MessageSubscriptionHandler(
            IProductService productService)
        {
            _productService = productService;
        }

        public async Task HandleAsync(GraphQLResponse<MessageSubscriptionResponse> @event)
        {
            var products = await _productService.GetProducts();
            return;
        }
    }
}