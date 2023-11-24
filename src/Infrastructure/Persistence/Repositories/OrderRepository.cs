using Domain.Entities;
using Infrastructure.Persistence.Common;
using Microsoft.Extensions.Options;

namespace Infrastructure.Persistence.Repositories;

public class OrderRepository(IOptions<MongoDbSettings> databaseSettings) : BaseRepository<Order>(databaseSettings);