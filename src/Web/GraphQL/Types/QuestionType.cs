using Application.EntityManagement.Answers.Queries;
using Application.EntityManagement.Users.Queries;
using Application.EntityManagement.Votes.Queries;
using Domain.Entities;
using Domain.Enums;
using MediatR;

namespace Web.GraphQL.Types;

public class QuestionType : ObjectType<Question>
{
    protected override void Configure(IObjectTypeDescriptor<Question> descriptor)
    {
        descriptor
            .Description(
                "Represents a question posted by a user, including details like title, description, associated user information, and answers.");

        descriptor
            .Field(question => question.User)
            .ResolveWith<Resolvers>(resolvers =>
                Resolvers.GetUserAsync(null!, null!))
            .Description("The User Who Posted the Question\n" +
                         "Authentication is required.");

        descriptor
            .Field(question => question.Answers)
            .ResolveWith<Resolvers>(resolvers =>
                Resolvers.GetAnswersAsync(null!, null!))
            .Description("The Answers Associated with the Question\n" +
                         "Supports sorting for answer details.");

        descriptor
            .Field(question => question.Votes)
            .ResolveWith<Resolvers>(resolvers =>
                Resolvers.GetVotesAsync(null!, null!))
            .Description("The Votes Received by the Question");

        descriptor
            .Field(question => question.DateCreated)
            .Description("The Creation Date");

        descriptor
            .Field(question => question.DateModified)
            .Description("The Last Modification Date");

        descriptor
            .Field(question => question.ExternalId)
            .Description("The External ID for Client Interactions");

        descriptor
            .Field(question => question.InternalId)
            .Ignore();

        descriptor
            .Field(question => question.UserId)
            .Ignore();

        descriptor
            .Field(question => question.ProductId)
            .Ignore();
    }

    private sealed class Resolvers
    {
        public async static Task<User?> GetUserAsync([Parent] Question question, [Service] ISender sender)
        {
            var usersQuery = new GetAllUsersQuery(x => x.InternalId == question.UserId);

            var result = await sender.Send(usersQuery);

            return result.Data?.FirstOrDefault();
        }

        public async static Task<IEnumerable<Answer>?> GetAnswersAsync([Parent] Question question,
            [Service] ISender sender)
        {
            var answersQuery = new GetAllAnswersQuery(x => x.QuestionId == question.InternalId);

            var result = await sender.Send(answersQuery);

            return result.Data;
        }

        public async static Task<IEnumerable<Vote>?> GetVotesAsync([Parent] Question question, [Service] ISender sender)
        {
            var votesQuery = new GetAllVotesQuery(x =>
                x.ContentId == question.InternalId && x.ContentType == VotableContentType.Question);

            var result = await sender.Send(votesQuery);

            return result.Data;
        }
    }
}