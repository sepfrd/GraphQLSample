using Domain.Entities;
using Infrastructure.Persistence.Common;
using Microsoft.Extensions.Options;

namespace Infrastructure.Persistence.Repositories;

public class UserRepository(IOptions<MongoDbSettings> databaseSettings) : BaseRepository<User>(databaseSettings);