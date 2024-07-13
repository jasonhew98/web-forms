using GraphQL;
using System.Threading.Tasks;

namespace Core.EventHub.GraphQL.Interfaces
{
    public interface ISubscriptionHandler<IRequest, IResponse>
    {
        Task HandleAsync(GraphQLResponse<IResponse> response);
    }
}