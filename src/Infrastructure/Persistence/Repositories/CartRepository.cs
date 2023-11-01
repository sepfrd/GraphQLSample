using Domain.Entities;
using Infrastructure.Persistence.Common;
using Microsoft.Extensions.Options;

namespace Infrastructure.Persistence.Repositories;

public class CartRepository : BaseRepository<Cart>
{
    public CartRepository(IOptions<MongoDbSettings> databaseSettings) : base(databaseSettings)
    {
    }
}