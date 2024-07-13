namespace Core.EventHub.GraphQL.Subscriptions
{
    public interface IRequest
    {
        string Query { get; }
    }
}