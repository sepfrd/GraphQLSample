using Domain.Entities;
using Infrastructure.Persistence.Common;
using Microsoft.Extensions.Options;

namespace Infrastructure.Persistence.Repositories;

public class CategoryRepository(IOptions<MongoDbSettings> databaseSettings)
    : BaseRepository<Category>(databaseSettings);