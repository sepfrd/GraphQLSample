using Domain.Entities;
using Infrastructure.Persistence.Common;
using Microsoft.Extensions.Options;

namespace Infrastructure.Persistence.Repositories;

public class ShipmentRepository : BaseRepository<Shipment>
{
    public ShipmentRepository(IOptions<MongoDbSettings> databaseSettings) : base(databaseSettings)
    {
    }
}