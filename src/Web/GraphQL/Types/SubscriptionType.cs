using Application.Abstractions;
using Application.Common.Constants;
using Application.EntityManagement.Answers.Dtos.AnswerDto;
using HotChocolate.Subscriptions;

namespace Web.GraphQL.Types;

public sealed class SubscriptionType : ObjectType<Subscription>
{
    protected override void Configure(IObjectTypeDescriptor<Subscription> descriptor)
    {
        descriptor
            .Field(_ => Subscription.OnAnswerSubmitted(default!))
            .Authorize(PolicyConstants.CustomerPolicy)
            .Subscribe(async context =>
            {
                var receiver = context.Service<ITopicEventReceiver>();
                var authenticationService = context.Service<IAuthenticationService>();

                var user = authenticationService.GetLoggedInUser();

                var topicName = $"{nameof(Subscription.OnAnswerSubmitted)}/{user!.Username}/questions/answers";

                var stream = await receiver.SubscribeAsync<AnswerDto>(topicName);

                return stream;
            })
            .Description("A Subscription API to Get Notified of Any New Answer Created");
    }
}