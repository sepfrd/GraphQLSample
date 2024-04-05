using System.Linq.Expressions;
using Application.Common;
using Domain.Entities;
using MediatR;

namespace Application.EntityManagement.Products.Queries;

public record GetAllProductsQuery(Expression<Func<Product, bool>>? Filter = null)
    : IRequest<QueryResponse<IEnumerable<Product>>>;