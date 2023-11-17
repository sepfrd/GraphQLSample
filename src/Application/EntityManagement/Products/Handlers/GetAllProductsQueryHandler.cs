using Application.Common.Handlers;
using Domain.Abstractions;
using Domain.Entities;

namespace Application.EntityManagement.Products.Handlers;

public class GetAllProductsQueryHandler(IRepository<Product> productRepository)
    : BaseGetAllQueryHandler<Product>(productRepository);