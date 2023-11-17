using Application.Abstractions;
using Application.Common.Handlers;
using Application.EntityManagement.Products.Dtos;
using Domain.Abstractions;
using Domain.Entities;
using Microsoft.Extensions.Logging;

namespace Application.EntityManagement.Products.Handlers;

public class CreateProductCommandHandler(
        IRepository<Product> repository,
        IMappingService mappingService,
        ILogger logger)
    : BaseCreateCommandHandler<Product, ProductDto>(repository, mappingService, logger);