using Application.EntityManagement.Products.Queries;
using Domain.Entities;
using MediatR;

namespace Web.GraphQL.Types;

public class CategoryType : ObjectType<Category>
{
    protected override void Configure(IObjectTypeDescriptor<Category> descriptor)
    {
        descriptor
            .Description(
                "Represents a product category with information such as name, description, image URL, and icon URL.");

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
                    Resolvers.GetProductsAsync(default!, default!))
            .UsePaging();

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
        public async static Task<IEnumerable<Product>?> GetProductsAsync([Parent] Category category,
            [Service] ISender sender)
        {
            var productsQuery = new GetAllProductsQuery(x => x.CategoryId == category.InternalId);

            var result = await sender.Send(productsQuery);

            return result.Data;
        }
    }
}