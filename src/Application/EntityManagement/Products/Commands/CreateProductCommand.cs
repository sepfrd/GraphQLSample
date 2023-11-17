using Application.Common.Commands;
using Application.EntityManagement.Products.Dtos;

namespace Application.EntityManagement.Products.Commands;

public record CreateProductCommand(ProductDto ProductDto) : BaseCreateCommand<ProductDto>(ProductDto);