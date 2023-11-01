using Domain.Entities;
using Infrastructure.Persistence.Common;
using Microsoft.Extensions.Options;

namespace Infrastructure.Persistence.Repositories;

public class CartItemRepository : BaseRepository<CartItem>
{
    public CartItemRepository(IOptions<MongoDbSettings> databaseSettings) : base(databaseSettings)
    {
    }
}