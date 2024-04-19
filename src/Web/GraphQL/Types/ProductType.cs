using Application.EntityManagement.Categories.Queries;
using Application.EntityManagement.Comments.Queries;
using Application.EntityManagement.Questions.Queries;
using Application.EntityManagement.Votes.Queries;
using Domain.Entities;
using Domain.Enums;
using MediatR;

namespace Web.GraphQL.Types;

public class ProductType : ObjectType<Product>
{
    protected override void Configure(IObjectTypeDescriptor<Product> descriptor)
    {
        descriptor
            .Description(
                "Represents a product with details such as name, description, price, stock quantity, images, associated category, and user comments.");

        descriptor
            .Field(product => product.Category)
            .ResolveWith<Resolvers>(
                resolvers =>
                    Resolvers.GetCategoryAsync(default!, default!))
            .Description("The Category Associated with the Product");

        descriptor
            .Field(product => product.Votes)
            .ResolveWith<Resolvers>(
                resolvers =>
                    Resolvers.GetVotesAsync(default!, default!))
            .Description("The Votes Associated with the Product");

        descriptor
            .Field(product => product.Comments)
            .ResolveWith<Resolvers>(
                resolvers =>
                    Resolvers.GetCommentsAsync(default!, default!))
            .Description("The Comments Associated with the Product\n" +
                         "Authentication is required.\n" +
                         "Supports sorting for comment details.");

        descriptor
            .Field(product => product.Questions)
            .ResolveWith<Resolvers>(
                resolvers =>
                    Resolvers.GetQuestionsAsync(default!, default!))
            .Description("The Questions Associated with the Product\n" +
                         "Authentication is required.\n" +
                         "Supports sorting for question details.");

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
        public async static Task<Category?> GetCategoryAsync([Parent] Product product, [Service] ISender sender)
        {
            var categoriesQuery = new GetAllCategoriesQuery(x => x.InternalId == product.CategoryId);

            var result = await sender.Send(categoriesQuery);

            return result.Data?.FirstOrDefault();
        }

        public async static Task<IEnumerable<Vote>?> GetVotesAsync([Parent] Product product, [Service] ISender sender)
        {
            var votesQuery = new GetAllVotesQuery(x => x.ContentId == product.InternalId && x.ContentType == VotableContentType.Product);

            var result = await sender.Send(votesQuery);

            return result.Data;
        }

        public async static Task<IEnumerable<Comment>?> GetCommentsAsync([Parent] Product product,
            [Service]
            ISender sender)
        {
            var commentsQuery = new GetAllCommentsQuery(x => x.ProductId == product.InternalId);

            var result = await sender.Send(commentsQuery);

            return result.Data;
        }

        public async static Task<IEnumerable<Question>?> GetQuestionsAsync([Parent] Product product,
            [Service]
            ISender sender)
        {
            var questionsQuery = new GetAllQuestionsQuery(x => x.ProductId == product.InternalId);

            var result = await sender.Send(questionsQuery);

            return result.Data;
        }
    }
}