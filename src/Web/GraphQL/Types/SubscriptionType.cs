using Application.EntityManagement.Answers.Dtos.AnswerDto;
using HotChocolate.Subscriptions;

namespace Web.GraphQL.Types;

public sealed class SubscriptionType : ObjectType<Subscription>
{
    protected override void Configure(IObjectTypeDescriptor<Subscription> descriptor)
    {
        descriptor
            .Field(_ => Subscription.OnAnswerSubmitted(default!))
            .Subscribe(async context =>
            {
                var receiver = context.Service<ITopicEventReceiver>();

                var stream = await receiver.SubscribeAsync<AnswerDto>(nameof(Subscription.OnAnswerSubmitted));

                return stream;
            })
            .Description("A Subscription API to Get Notified of Any New Answer Created");
    }
}