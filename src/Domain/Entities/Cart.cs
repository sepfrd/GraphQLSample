using Domain.Abstractions;
using Domain.Common;

namespace Domain.Entities;

public sealed class Cart : BaseEntity
{
    private readonly IDbContext _dbContext;
    private List<CartItem>? _cartItems;

    public Cart(IDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public ICollection<Guid>? CartItemIds { get; set; }

    public Guid UserId { get; set; }

    public decimal TotalPrice
    {
        get
        {
            GetCartItems();

            if (_cartItems is null || _cartItems.Count == 0)
            {
                return 0;
            }

            return _cartItems.Sum(item => item.SubTotalPrice);
        }
    }

    private void GetCartItems()
    {
        if (_cartItems is not null &&
            CartItemIds is not null &&
            CartItemIds.Count != 0)
        {
            return;
        }

        if (CartItemIds is null || CartItemIds.Count == 0)
        {
            _cartItems = null;

            return;
        }

        _cartItems = _dbContext.CartItems
            .Where(item => CartItemIds.Contains(item.InternalId))
            .ToList();
    }
}