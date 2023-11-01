using Domain.Entities;
using Infrastructure.Persistence.Common;
using Microsoft.Extensions.Options;

namespace Infrastructure.Persistence.Repositories;

public class ProductRepository : BaseRepository<Product>
{
    public ProductRepository(IOptions<MongoDbSettings> databaseSettings) : base(databaseSettings)
    {
    }
}