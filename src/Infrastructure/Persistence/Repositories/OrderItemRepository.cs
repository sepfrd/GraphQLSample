using Domain.Entities;
using Infrastructure.Persistence.Common;
using Microsoft.Extensions.Options;

namespace Infrastructure.Persistence.Repositories;

public class OrderItemRepository : BaseRepository<OrderItem>
{
    public OrderItemRepository(IOptions<MongoDbSettings> databaseSettings) : base(databaseSettings)
    {
    }
}