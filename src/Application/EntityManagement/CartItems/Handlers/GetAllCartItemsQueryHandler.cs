using Application.Common.Handlers;
using Domain.Abstractions;
using Domain.Entities;

namespace Application.EntityManagement.CartItems.Handlers;

public class GetAllCartItemsQueryHandler(IRepository<CartItem> cartItemRepository)
    : BaseGetAllQueryHandler<CartItem>(cartItemRepository);