using Application.Common;
using Application.EntityManagement.Categories.Dtos.CategoryDto;
using MediatR;

namespace Application.EntityManagement.Categories.Commands;

public record CreateCategoryCommand(CategoryDto CategoryDto) : IRequest<CommandResult>;