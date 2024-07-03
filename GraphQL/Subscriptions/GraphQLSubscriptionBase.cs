using GraphQL;
using System;

namespace WebForms.GraphQL.Subscriptions
{
    public abstract class GraphQLSubscriptionBase<T>
    {
        public abstract string Query { get; }
        public abstract Action<GraphQLResponse<T>> OnReceive { get; }
    }
}