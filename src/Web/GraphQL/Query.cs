using Application.Common;
using Application.EntityManagement.Categories.Queries;
using Application.EntityManagement.Products.Queries;
using Application.EntityManagement.Users.Queries;
using Domain.Entities;
using MediatR;

namespace Web.GraphQL;

public class Query
{
    public async Task<IEnumerable<Category>?> GetCategoriesAsync(Pagination? pagination, [Service] ISender sender, CancellationToken cancellationToken)
    {
        pagination ??= new Pagination();
        
        var categoryQuery = new GetAllCategoriesQuery(pagination);

        var result = await sender.Send(categoryQuery, cancellationToken);

        return result.Data;
    }

    public async Task<IEnumerable<Product>?> GetProductsAsync(Pagination? pagination, [Service] ISender sender, CancellationToken cancellationToken)
    {
        pagination ??= new Pagination();

        var productQuery = new GetAllProductsQuery(pagination);

        var result = await sender.Send(productQuery, cancellationToken);

        return result.Data;
    }

    public async Task<IEnumerable<User>?> GetUsersAsync(Pagination? pagination, [Service] ISender sender, CancellationToken cancellationToken)
    {
        pagination ??= new Pagination();
        
        var usersQuery = new GetAllUsersQuery(pagination);

        var result = await sender.Send(usersQuery, cancellationToken);

        return result.Data;
    }
}