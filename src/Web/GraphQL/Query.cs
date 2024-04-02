using System.Linq.Expressions;
using Application.EntityManagement.Categories.Queries;
using Application.EntityManagement.Products.Queries;
using Application.EntityManagement.Users.Queries;
using Domain.Common;
using Domain.Entities;
using Domain.Filters;
using MediatR;

namespace Web.GraphQL;

public class Query
{
    public static async Task<IEnumerable<Category>?> GetCategoriesAsync(Pagination? pagination, [Service] ISender sender, CancellationToken cancellationToken)
    {
        pagination ??= new Pagination();

        var categoryQuery = new GetAllCategoriesQuery(pagination);

        var result = await sender.Send(categoryQuery, cancellationToken);

        return result.Data;
    }

    public static async Task<IEnumerable<Product>?> GetProductsAsync(Pagination? pagination, CustomProductFilter? productFilter, [Service] ISender sender,
        CancellationToken cancellationToken)
    {
        pagination ??= new Pagination();

        Expression<Func<Product, bool>>? filter = null;

        if (productFilter is not null)
        {
            filter = BuildProductFilterExpression(productFilter);
        }

        var productQuery = new GetAllProductsQuery(pagination, filter);

        var result = await sender.Send(productQuery, cancellationToken);

        return result.Data;
    }

    public static async Task<IEnumerable<User>?> GetUsersAsync(Pagination? pagination, CustomUserFilter? userFilter, [Service] ISender sender, CancellationToken cancellationToken)
    {
        pagination ??= new Pagination();

        Expression<Func<User, bool>>? filter = null;

        if (userFilter is not null)
        {
            filter = BuildUserFilterExpression(userFilter);
        }

        var usersQuery = new GetAllUsersQuery(pagination, filter);

        var result = await sender.Send(usersQuery, cancellationToken);

        return result.Data;
    }

    private static Expression<Func<User, bool>>? BuildUserFilterExpression(CustomUserFilter userFilter)
    {
        var user = Expression.Parameter(typeof(User), "user");

        var expressions = new List<BinaryExpression>();

        if (userFilter.ExternalId is not null)
        {
            var externalIdMember = Expression.Property(user, nameof(User.ExternalId));
            var externalIdConstant = Expression.Constant(userFilter.ExternalId);

            expressions.Add(Expression.Equal(externalIdMember, externalIdConstant));
        }

        if (userFilter.Email is not null)
        {
            var emailMember = Expression.Property(user, nameof(User.Email));
            var emailConstant = Expression.Constant(userFilter.Email);

            expressions.Add(Expression.Equal(emailMember, emailConstant));
        }

        if (userFilter.Username is not null)
        {
            var usernameMember = Expression.Property(user, nameof(User.Username));
            var usernameConstant = Expression.Constant(userFilter.Username);

            expressions.Add(Expression.Equal(usernameMember, usernameConstant));
        }

        BinaryExpression? baseExpression = null;

        foreach (var expressionItem in expressions)
        {
            baseExpression = baseExpression switch
            {
                null => Expression.Equal(expressionItem.Left, expressionItem.Right),
                _ => Expression.AndAlso(baseExpression, expressionItem)
            };
        }

        if (baseExpression is null)
        {
            return null;
        }

        var lambda = Expression.Lambda<Func<User, bool>>(baseExpression, user);

        return lambda;
    }

    private static Expression<Func<Product, bool>>? BuildProductFilterExpression(CustomProductFilter productFilter)
    {
        var product = Expression.Parameter(typeof(Product), "product");

        var expressions = new List<BinaryExpression>();

        if (productFilter.ExternalId is not null)
        {
            var externalIdMember = Expression.Property(product, nameof(Product.ExternalId));
            var externalIdConstant = Expression.Constant(productFilter.ExternalId);

            expressions.Add(Expression.Equal(externalIdMember, externalIdConstant));
        }

        if (productFilter.Name is not null)
        {
            var nameMember = Expression.Property(product, nameof(Product.Name));
            var nameConstant = Expression.Constant(productFilter.Name);

            expressions.Add(Expression.Equal(nameMember, nameConstant));
        }

        if (productFilter.Description is not null)
        {
            var productDescriptionMember = Expression.Property(product, nameof(Product.Description));
            var productDescriptionConstant = Expression.Constant(productFilter.Description);

            expressions.Add(Expression.Equal(productDescriptionMember, productDescriptionConstant));
        }

        if (productFilter.Price is not null)
        {
            var productPriceMember = Expression.Property(product, nameof(Product.Price));
            var productPriceConstant = Expression.Constant(productFilter.Price);

            expressions.Add(Expression.Equal(productPriceMember, productPriceConstant));
        }

        BinaryExpression? baseExpression = null;

        foreach (var expressionItem in expressions)
        {
            baseExpression = baseExpression switch
            {
                null => Expression.Equal(expressionItem.Left, expressionItem.Right),
                _ => Expression.AndAlso(baseExpression, expressionItem)
            };
        }

        if (baseExpression is null)
        {
            return null;
        }

        var lambda = Expression.Lambda<Func<Product, bool>>(baseExpression, product);

        return lambda;
    }

}