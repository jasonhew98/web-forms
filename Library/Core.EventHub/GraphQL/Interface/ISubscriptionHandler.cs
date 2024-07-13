using GraphQL;
using System.Threading.Tasks;

namespace Core.EventHub.GraphQL.Interface
{
    public interface ISubscriptionHandler<IRequest, IResponse>
    {
        Task HandleAsync(GraphQLResponse<IResponse> response);
    }
}