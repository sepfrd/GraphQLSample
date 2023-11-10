using Application.Common;
using Application.EntityManagement.Categories.Queries;
using Application.EntityManagement.Users.Queries;
using Application.EntityManagement.Votes.Queries;
using Domain.Entities;
using HotChocolate;
using HotChocolate.Types;
using MediatR;

namespace Web.GraphQL.Types;

public class ProductType : ObjectType<Product>
{
    protected override void Configure(IObjectTypeDescriptor<Product> descriptor)
    {
        descriptor
            .Field(product => product.Category)
            .ResolveWith<Resolvers>(
                resolvers =>
                    resolvers.GetCategoryAsync(default!, default!));

        descriptor
            .Field(product => product.Votes)
            .ResolveWith<Resolvers>(
                resolvers =>
                    resolvers.GetVotesAsync(default!, default!));
        
        descriptor
            .Field(product => product.DateCreated)
            .Description("The Creation Date");

        descriptor
            .Field(product => product.DateModified)
            .Description("The Last Modification Date");

        descriptor
            .Field(product => product.ExternalId)
            .Description("The External ID for Client Interactions");

        descriptor
            .Field(product => product.InternalId)
            .Ignore();

        descriptor
            .Field(product => product.CategoryId)
            .Ignore();
    }

    private sealed class Resolvers
    {
        public async Task<Category?> GetCategoryAsync([Parent] Product product, [Service] ISender sender)
        {
            var categoriesQuery = new GetAllCategoriesQuery(
                new Pagination(),
                null,
                x => x.InternalId == product.CategoryId);

            var result = await sender.Send(categoriesQuery);

            return result.Data?.FirstOrDefault();
        }
        
        public async Task<IEnumerable<Vote>?> GetVotesAsync([Parent] Product product, [Service] ISender sender)
        {
            var votesQuery = new GetAllVotesQuery(
                new Pagination(),
                null,
                x => x.ContentId == product.InternalId && x.Content is Product);

            var result = await sender.Send(votesQuery);

            return result.Data;
        }
    }
}