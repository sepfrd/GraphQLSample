using Application.Common;
using Application.EntityManagement.Products.Queries;
using Application.EntityManagement.Users.Queries;
using Application.EntityManagement.Votes.Queries;
using Domain.Entities;
using MediatR;

namespace Web.GraphQL.Types;

public class CommentType : ObjectType<Comment>
{
    protected override void Configure(IObjectTypeDescriptor<Comment> descriptor)
    {
        descriptor.Description("Represents a product comment.");

        descriptor
            .Field(comment => comment.Description)
            .Description("The Comment Text");

        descriptor
            .Field(comment => comment.Product)
            .Description("The Product This Comment Is For")
            .ResolveWith<Resolvers>(
                resolvers =>
                    resolvers.GetProductAsync(default!, default!));

        descriptor
            .Field(comment => comment.User)
            .Description("The User Who Wrote This Comment")
            .ResolveWith<Resolvers>(
                resolvers =>
                    resolvers.GetUserAsync(default!, default!));

        descriptor
            .Field(comment => comment.Votes)
            .Description("The Votes Submitted for This Comment")
            .ResolveWith<Resolvers>(
                resolvers =>
                    resolvers.GetVotesAsync(default!, default!));

        descriptor
            .Field(comment => comment.DateCreated)
            .Description("The Creation Date");

        descriptor
            .Field(comment => comment.DateModified)
            .Description("The Last Modification Date");

        descriptor
            .Field(comment => comment.ExternalId)
            .Description("The External ID for Client Interactions");

        descriptor
            .Field(comment => comment.InternalId)
            .Ignore();

        descriptor
            .Field(comment => comment.UserId)
            .Ignore();

        descriptor
            .Field(comment => comment.ProductId)
            .Ignore();
    }

    private sealed class Resolvers
    {
        public async Task<Product?> GetProductAsync([Parent] Comment comment, [Service] ISender sender)
        {
            var productsQuery = new GetAllProductsQuery(
                new Pagination(),
                null,
                x => x.InternalId == comment.ProductId);

            var result = await sender.Send(productsQuery);

            return result.Data?.FirstOrDefault();
        }

        public async Task<User?> GetUserAsync([Parent] Comment comment, [Service] ISender sender)
        {
            var usersQuery = new GetAllUsersQuery(
                new Pagination(),
                null,
                x => x.InternalId == comment.UserId);

            var result = await sender.Send(usersQuery);

            return result.Data?.FirstOrDefault();
        }

        public async Task<IEnumerable<Vote>?> GetVotesAsync([Parent] Comment comment, [Service] ISender sender)
        {
            var votesQuery = new GetAllVotesQuery(
                new Pagination(),
                null,
                x => x.ContentId == comment.InternalId && x.Content is Comment);

            var result = await sender.Send(votesQuery);

            return result.Data;
        }
    }
}