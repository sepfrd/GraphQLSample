using Application.EntityManagement.Answers.Queries;
using Application.EntityManagement.Users.Queries;
using Application.EntityManagement.Votes.Queries;
using Domain.Common;
using Domain.Entities;
using MediatR;

namespace Web.GraphQL.Types;

public class QuestionType : ObjectType<Question>
{
    protected override void Configure(IObjectTypeDescriptor<Question> descriptor)
    {
        descriptor
            .Field(question => question.User)
            .ResolveWith<Resolvers>(
                resolvers =>
                    Resolvers.GetUserAsync(default!, default!));

        descriptor
            .Field(question => question.Answers)
            .ResolveWith<Resolvers>(
                resolvers =>
                    Resolvers.GetAnswersAsync(default!, default!))
            .UseFiltering()
            .UseSorting();

        descriptor
            .Field(question => question.Votes)
            .ResolveWith<Resolvers>(
                resolvers =>
                    Resolvers.GetVotesAsync(default!, default!));

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
    }

    private sealed class Resolvers
    {
        public static async Task<User?> GetUserAsync([Parent] Question question, [Service] ISender sender)
        {
            var usersQuery = new GetAllUsersQuery(
                new Pagination(),
                x => x.InternalId == question.UserId);

            var result = await sender.Send(usersQuery);

            return result.Data?.FirstOrDefault();
        }

        public static async Task<IEnumerable<Answer>?> GetAnswersAsync([Parent] Question question, [Service] ISender sender)
        {
            var answersQuery = new GetAllAnswersQuery(
                new Pagination(),
                x => x.QuestionId == question.InternalId);

            var result = await sender.Send(answersQuery);

            return result.Data;
        }

        public static async Task<IEnumerable<Vote>?> GetVotesAsync([Parent] Question question, [Service] ISender sender)
        {
            var votesQuery = new GetAllVotesQuery(
                new Pagination(),
                x => x.ContentId == question.InternalId && x.Content is Question);

            var result = await sender.Send(votesQuery);

            return result.Data;
        }
    }
}