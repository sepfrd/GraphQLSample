#region

using Domain.Entities;
using Infrastructure.Persistence.Common;
using Microsoft.Extensions.Options;

#endregion

namespace Infrastructure.Persistence.Repositories;

public class UserRoleRepository(IOptions<MongoDbSettings> databaseSettings) : BaseRepository<UserRole>(databaseSettings);