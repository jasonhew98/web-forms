using GraphQL;
using System.Threading.Tasks;
using WebForms.GraphQL.Interface;
using WebForms.Services.Interface;

namespace WebForms.GraphQL.Subscriptions
{
    public class MessageSubscriptionResponse : IResponse
    {
        public MessageReceive OnMessageReceived { get; set; }

        public class MessageReceive
        {
            public string Text { get; set; }
        }
    }

    public class MessageSubscription : IRequest
    {
        public string Query => @"
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