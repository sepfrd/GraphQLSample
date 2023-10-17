using Application.Common;
using MediatR;

namespace Application.EntityManagement.Categories.Queries;

public record GetAllCategoriesQuery : IRequest<QueryResponse>;