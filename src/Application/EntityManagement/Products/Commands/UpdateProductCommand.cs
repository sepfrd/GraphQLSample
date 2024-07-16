using Application.Common;
using Application.EntityManagement.Products.Dtos.ProductDto;
using MediatR;

namespace Application.EntityManagement.Products.Commands;

public record UpdateProductCommand(int ExternalId, ProductDto ProductDto) : IRequest<CommandResult>;