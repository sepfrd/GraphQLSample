using Application.EntityManagement.Answers.Queries;
using Application.EntityManagement.Comments.Queries;
using Application.EntityManagement.Products.Queries;
using Application.EntityManagement.Questions.Queries;
using Application.EntityManagement.Users.Queries;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;

namespace Web.GraphQL.Types;

public class VoteType : ObjectType<Vote>
{
    protected override void Configure(IObjectTypeDescriptor<Vote> descriptor)
    {
        descriptor
            .Description(
                "Represents a user's vote on content, including details like vote type, content type, and the external ID of the voted content.");

        descriptor
            .Field(vote => vote.User)
            .ResolveWith<Resolvers>(
                resolvers =>
                    Resolvers.GetUserAsync(default!, default!))
            .Description("The User Who Voted\n" +
                         "Authentication is required.");

        descriptor
            .Field(vote => vote.Content)
            .ResolveWith<Resolvers>(
                resolvers =>
                    Resolvers.GetContentAsync(default!, default!))
            .Description("The Content Voted On\n" +
                         "Authentication is required.");

        descriptor
            .Field(vote => vote.DateCreated)
            .Description("The Creation Date");

        descriptor
            .Field(vote => vote.DateModified)
            .Description("The Last Modification Date");

        descriptor
            .Field(vote => vote.ExternalId)
            .Description("The External ID for Client Interactions");

        descriptor
            .Field(vote => vote.InternalId)
            .Ignore();

        descriptor
            .Field(vote => vote.UserId)
            .Ignore();

        descriptor
            .Field(vote => vote.ContentId)
            .Ignore();
    }

    private sealed class Resolvers
    {
        public static async Task<User?> GetUserAsync([Parent] Vote vote, [Service] ISender sender)
        {
            var usersQuery = new GetAllUsersQuery(x => x.InternalId == vote.UserId);

            var result = await sender.Send(usersQuery);

            return result.Data?.FirstOrDefault();
        }

        public static async Task<IVotableContent?> GetContentAsync([Parent] Vote vote, [Service] ISender sender)
        {
            switch (vote.Content)
            {
                case Answer:
                {
                    var contentsQuery = new GetAllAnswersQuery(x => x.InternalId == vote.ContentId);

                    var result = await sender.Send(contentsQuery);

                    return result.Data?.FirstOrDefault();
                }

                case Comment:
                {
                    var contentsQuery = new GetAllCommentsQuery(x => x.InternalId == vote.ContentId);

                    var result = await sender.Send(contentsQuery);

                    return result.Data?.FirstOrDefault();
                }

                case Product:
                {
                    var contentsQuery = new GetAllProductsQuery(x => x.InternalId == vote.ContentId);

                    var result = await sender.Send(contentsQuery);

                    return result.Data?.FirstOrDefault();
                }

                case Question:
                {
                    var contentsQuery = new GetAllQuestionsQuery(x => x.InternalId == vote.ContentId);

                    var result = await sender.Send(contentsQuery);

                    return result.Data?.FirstOrDefault();
                }

                default:
                    return null;
            }
        }
    }
}