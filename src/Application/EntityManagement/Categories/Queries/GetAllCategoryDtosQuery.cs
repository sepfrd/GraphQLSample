using Application.Common;
using Application.EntityManagement.Categories.Dtos;
using Domain.Common;
using MediatR;

namespace Application.EntityManagement.Categories.Queries;

public record GetAllCategoryDtosQuery(Pagination? Pagination) : IRequest<QueryReferenceResponse<IEnumerable<CategoryDto>>>;