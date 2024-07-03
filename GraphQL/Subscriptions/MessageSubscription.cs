using GraphQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebForms.GraphQL.Subscriptions
{
    public class MessageSubscriptionResponse
    {
        public MessageReceive OnMessageReceived { get; set; }

        public class MessageReceive
        {
            public string Text { get; set; }
        }
    }

    public class MessageSubscription : GraphQLSubscriptionBase<MessageSubscriptionResponse>
    {
        public override string Query => @"
            subscription {
                onMessageReceived {
                    text
                }
            }";

        public override Action<GraphQLResponse<MessageSubscriptionResponse>> OnReceive => response =>
        {
            Console.WriteLine($"User '{response?.Data?.OnMessageReceived.Text}' joined");
        };
    }
}