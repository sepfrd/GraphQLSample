using Domain.Entities;
using Infrastructure.Persistence.Common;
using Microsoft.Extensions.Options;

namespace Infrastructure.Persistence.Repositories;

public class CartRepository(IOptions<MongoDbSettings> databaseSettings) : BaseRepository<Cart>(databaseSettings);