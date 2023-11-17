using Application.Common;
using Application.EntityManagement.Categories.Dtos;
using MediatR;

namespace Application.EntityManagement.Categories.Queries;

public record GetAllCategoryDtosQuery : IRequest<QueryReferenceResponse<IEnumerable<CategoryDto>>>;