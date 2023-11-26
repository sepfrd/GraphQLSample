#region

using Application.Common;
using Application.EntityManagement.Products.Dtos;
using MediatR;

#endregion

namespace Application.EntityManagement.Products.Commands;

public record CreateProductCommand(CreateProductDto CreateProductDto) : IRequest<CommandResult>;