using Domain.Entities;
using Infrastructure.Persistence.Common;
using Microsoft.Extensions.Options;

namespace Infrastructure.Persistence.Repositories;

public class CategoryRepository : BaseRepository<Category>
{
    public CategoryRepository(IOptions<MongoDbSettings> databaseSettings) : base(databaseSettings)
    {
    }
}