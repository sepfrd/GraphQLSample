using Application.Common;
using Application.EntityManagement.Products.Queries;
using Domain.Entities;
using HotChocolate;
using HotChocolate.Types;
using MediatR;

namespace Web.GraphQL.Types;

public class CategoryType : ObjectType<Category>
{
    protected override void Configure(IObjectTypeDescriptor<Category> descriptor)
    {
        descriptor.Description("Represents a category in its entirety.");

        descriptor
            .Field(category => category.Name)
            .Description("The Name of This Category");

        descriptor
            .Field(category => category.Description)
            .Description("The Description of This Category");

        descriptor
            .Field(category => category.IconUrl)
            .Description("The Url of This Category Icon");

        descriptor
            .Field(category => category.ImageUrl)
            .Description("The Url of This Category Image");

        descriptor
            .Field(category => category.Products)
            .Description("All Products of This Category")
            .ResolveWith<Resolvers>(
                resolvers =>
                    resolvers.GetProductsAsync(default!, default!));

        descriptor
            .Field(category => category.DateCreated)
            .Description("The Creation Date");

        descriptor
            .Field(category => category.DateModified)
            .Description("The Last Modification Date");

        descriptor
            .Field(category => category.ExternalId)
            .Description("The External ID for Client Interactions");

        descriptor
            .Field(category => category.InternalId)
            .Ignore();
    }

    private sealed class Resolvers
    {
        public async Task<IEnumerable<Product>?> GetProductsAsync([Parent] Category category, [Service] ISender sender)
        {
            var productsQuery = new GetAllProductsQuery(
                new Pagination(),
                null,
                x => x.CategoryId == category.InternalId);

            var result = await sender.Send(productsQuery);

            return result.Data;
        }
    }
}