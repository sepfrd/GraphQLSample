using Application.Common.Handlers;
using Domain.Abstractions;
using Domain.Entities;
using Microsoft.Extensions.Logging;

namespace Application.EntityManagement.Products.Handlers;

public class DeleteProductByExternalIdCommandHandler(
        IRepository<Product> repository,
        ILogger logger)
    : BaseDeleteByExternalIdCommandHandler<Product>(repository, logger);