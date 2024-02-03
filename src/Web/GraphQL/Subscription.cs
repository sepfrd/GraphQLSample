using Application.EntityManagement.Answers.Dtos.AnswerDto;
using HotChocolate.Resolvers;

namespace Web.GraphQL;

public class Subscription
{
    public static AnswerDto OnAnswerSubmitted(IResolverContext resolverContext) =>
        resolverContext.GetEventMessage<AnswerDto>();
}