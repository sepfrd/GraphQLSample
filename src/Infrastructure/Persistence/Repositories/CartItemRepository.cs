using Domain.Entities;
using Infrastructure.Persistence.Common;
using Microsoft.Extensions.Options;

namespace Infrastructure.Persistence.Repositories;

public class CartItemRepository(IOptions<MongoDbSettings> databaseSettings) : BaseRepository<CartItem>(databaseSettings);