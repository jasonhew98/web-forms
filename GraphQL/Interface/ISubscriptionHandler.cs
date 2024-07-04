using GraphQL;
using System.Threading.Tasks;
using WebForms.GraphQL.Subscriptions;

namespace WebForms.GraphQL.Interface
{
    public interface ISubscriptionHandler<TSubscription, TSubscriptionResponse>
        where TSubscription : Subscription
        where TSubscriptionResponse : SubscriptionResponse
    {
        Task HandleAsync(GraphQLResponse<TSubscriptionResponse> response);
    }
}