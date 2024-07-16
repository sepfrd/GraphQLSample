using Application.EntityManagement.Categories.Queries;
using Application.EntityManagement.Products.Queries;
using Application.EntityManagement.Users.Queries;
using Domain.Entities;
using MediatR;
using MethodTimer;

namespace Web.GraphQL;

public class Query
{
    [Time]
    public async static Task<IEnumerable<Category>?> GetCategoriesAsync([Service] ISender sender,
        CancellationToken cancellationToken)
    {
        var categoryQuery = new GetAllCategoriesQuery();

        var result = await sender.Send(categoryQuery, cancellationToken);

        return result.Data;
    }

    [Time]
    public async static Task<IEnumerable<Product>?> GetProductsAsync([Service] ISender sender,
        CancellationToken cancellationToken)
    {
        var productQuery = new GetAllProductsQuery();

        var result = await sender.Send(productQuery, cancellationToken);

        return result.Data;
    }

    [Time]
    public async static Task<IEnumerable<User>?> GetUsersAsync([Service] ISender sender,
        CancellationToken cancellationToken)
    {
        var usersQuery = new GetAllUsersQuery();

        var result = await sender.Send(usersQuery, cancellationToken);

        return result.Data;
    }
}