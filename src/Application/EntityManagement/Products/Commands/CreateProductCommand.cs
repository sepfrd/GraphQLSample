using Application.Common;
using Application.EntityManagement.Products.Dtos;
using MediatR;

namespace Application.EntityManagement.Products.Commands;

public record CreateProductCommand(CreateProductDto CreateProductDto) : IRequest<CommandResult>;