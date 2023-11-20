using Application.Common;
using Application.EntityManagement.Categories.Queries;
using Application.EntityManagement.Products.Queries;
using Application.EntityManagement.Users.Queries;
using Domain.Entities;
using MediatR;
using System.Linq.Expressions;

namespace Web.GraphQL;

public class Query
{
    public async Task<IEnumerable<Category>?> GetCategoriesAsync([Service] ISender sender, CancellationToken cancellationToken)
    {
        var pagination = new Pagination(1, int.MaxValue);

        var relationsToInclude = new Expression<Func<Category, object?>>[]
        {
            category => category.Products
        };

        var categoryQuery = new GetAllCategoriesQuery(pagination, relationsToInclude);

        var result = await sender.Send(categoryQuery, cancellationToken);

        return result.Data;
    }

    public async Task<IEnumerable<Product>?> GetProductsAsync([Service] ISender sender, CancellationToken cancellationToken)
    {
        var pagination = new Pagination(1, int.MaxValue);

        var relationsToInclude = new Expression<Func<Product, object?>>[]
        {
            product => product.Category,
            product => product.Comments,
            product => product.Questions,
            product => product.Votes
        };

        var productQuery = new GetAllProductsQuery(pagination, relationsToInclude);

        var result = await sender.Send(productQuery, cancellationToken);

        return result.Data;
    }

    public async Task<IEnumerable<User>?> GetUsersAsync([Service] ISender sender, CancellationToken cancellationToken)
    {
        var pagination = new Pagination(1, int.MaxValue);
        
        var usersQuery = new GetAllUsersQuery(pagination);

        var result = await sender.Send(usersQuery, cancellationToken);

        return result.Data;
    }
}