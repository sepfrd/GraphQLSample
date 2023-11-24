using Domain.Entities;
using Infrastructure.Persistence.Common;
using Microsoft.Extensions.Options;

namespace Infrastructure.Persistence.Repositories;

public class AnswerRepository(IOptions<MongoDbSettings> databaseSettings) : BaseRepository<Answer>(databaseSettings);