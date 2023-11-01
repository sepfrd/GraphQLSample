using Domain.Entities;
using Infrastructure.Persistence.Common;
using Microsoft.Extensions.Options;

namespace Infrastructure.Persistence.Repositories;

public class QuestionRepository : BaseRepository<Question>
{
    public QuestionRepository(IOptions<MongoDbSettings> databaseSettings) : base(databaseSettings)
    {
    }
}