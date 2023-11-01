using Domain.Entities;
using Infrastructure.Persistence.Common;
using Microsoft.Extensions.Options;

namespace Infrastructure.Persistence.Repositories;

public class RoleRepository : BaseRepository<Role>
{
    public RoleRepository(IOptions<MongoDbSettings> databaseSettings) : base(databaseSettings)
    {
    }
}