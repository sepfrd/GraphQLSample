using Domain.Entities;
using Infrastructure.Persistence.Common;
using Microsoft.Extensions.Options;

namespace Infrastructure.Persistence.Repositories;

public class VoteRepository(IOptions<MongoDbSettings> databaseSettings) : BaseRepository<Vote>(databaseSettings);