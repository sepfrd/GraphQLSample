using Domain.Entities;
using Infrastructure.Persistence.Common;
using Microsoft.Extensions.Options;

namespace Infrastructure.Persistence.Repositories;

public class QuestionRepository(IOptions<MongoDbSettings> databaseSettings)
    : BaseRepository<Question>(databaseSettings);