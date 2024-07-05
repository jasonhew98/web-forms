using GraphQL;
using System.Threading.Tasks;

namespace WebForms.GraphQL.Interface
{
    public interface ISubscriptionHandler<IRequest, IResponse>
    {
        Task HandleAsync(GraphQLResponse<IResponse> response);
    }
}