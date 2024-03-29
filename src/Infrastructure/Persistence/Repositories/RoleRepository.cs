using Domain.Entities;
using Infrastructure.Persistence.Common;
using Microsoft.Extensions.Options;

namespace Infrastructure.Persistence.Repositories;

public class RoleRepository(IOptions<MongoDbSettings> databaseSettings) : BaseRepository<Role>(databaseSettings);