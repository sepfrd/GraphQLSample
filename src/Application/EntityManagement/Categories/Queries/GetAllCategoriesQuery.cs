using System.Linq.Expressions;
using Application.Common;
using Domain.Entities;
using MediatR;

namespace Application.EntityManagement.Categories.Queries;

public record GetAllCategoriesQuery(Expression<Func<Category, bool>>? Filter = null)
    : IRequest<QueryResponse<IEnumerable<Category>>>;