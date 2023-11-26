#region

using Application.Common;
using Application.EntityManagement.Categories.Dtos;
using MediatR;

#endregion

namespace Application.EntityManagement.Categories.Commands;

public record CreateCategoryCommand(CategoryDto CategoryDto) : IRequest<CommandResult>;