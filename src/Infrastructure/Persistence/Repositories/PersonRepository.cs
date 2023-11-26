#region

using Domain.Entities;
using Infrastructure.Persistence.Common;
using Microsoft.Extensions.Options;

#endregion

namespace Infrastructure.Persistence.Repositories;

public class PersonRepository(IOptions<MongoDbSettings> databaseSettings) : BaseRepository<Person>(databaseSettings);