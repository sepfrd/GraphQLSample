using Domain.Entities;
using Infrastructure.Persistence.Common;
using Microsoft.Extensions.Options;

namespace Infrastructure.Persistence.Repositories;

public class ProductRepository(IOptions<MongoDbSettings> databaseSettings) : BaseRepository<Product>(databaseSettings);