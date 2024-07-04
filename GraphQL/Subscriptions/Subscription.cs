namespace WebForms.GraphQL.Subscriptions
{
    public abstract class Subscription
    {
        public abstract string Query { get; }
    }
}