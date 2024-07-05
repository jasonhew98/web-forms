using GraphQL;
using System.Threading.Tasks;
using WebForms.GraphQL.Subscriptions;

namespace WebForms.GraphQL.Interface
{
    public interface ISubscriptionHandler<TSubscription, TSubscriptionResponse>
        where TSubscription : IQuery
        where TSubscriptionResponse : ISubscriptionResponse
    {
        Task HandleAsync(GraphQLResponse<TSubscriptionResponse> response);
    }
}