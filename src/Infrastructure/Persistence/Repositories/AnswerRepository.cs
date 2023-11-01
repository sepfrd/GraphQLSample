using Domain.Entities;
using Infrastructure.Persistence.Common;
using Microsoft.Extensions.Options;

namespace Infrastructure.Persistence.Repositories;

public class AnswerRepository : BaseRepository<Answer>
{
    public AnswerRepository(IOptions<MongoDbSettings> databaseSettings) : base(databaseSettings)
    {
    }
}