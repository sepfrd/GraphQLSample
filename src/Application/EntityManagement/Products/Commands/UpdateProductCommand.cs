using Application.Common.Commands;
using Application.EntityManagement.Products.Dtos;

namespace Application.EntityManagement.Products.Commands;

public record UpdateProductCommand(int ExternalId, ProductDto ProductDto) : BaseUpdateCommand<ProductDto>(ExternalId, ProductDto);