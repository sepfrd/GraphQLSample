using Domain.Entities;
using Infrastructure.Persistence.Common;
using Microsoft.Extensions.Options;

namespace Infrastructure.Persistence.Repositories;

public class ShipmentRepository(IOptions<MongoDbSettings> databaseSettings) : BaseRepository<Shipment>(databaseSettings);