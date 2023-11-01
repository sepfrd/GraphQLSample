using Domain.Entities;
using Infrastructure.Persistence.Common;
using Microsoft.Extensions.Options;

namespace Infrastructure.Persistence.Repositories;

public class OrderRepository : BaseRepository<Order>
{
    public OrderRepository(IOptions<MongoDbSettings> databaseSettings) : base(databaseSettings)
    {
    }
}