using Application.Common.Handlers;
using Domain.Abstractions;
using Domain.Entities;

namespace Application.EntityManagement.Carts.Handlers;

public class GetAllCartsQueryHandler(IRepository<Cart> cartRepository)
    : BaseGetAllQueryHandler<Cart>(cartRepository);