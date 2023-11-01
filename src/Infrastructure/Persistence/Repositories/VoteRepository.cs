using Domain.Entities;
using Infrastructure.Persistence.Common;
using Microsoft.Extensions.Options;

namespace Infrastructure.Persistence.Repositories;

public class VoteRepository : BaseRepository<Vote>
{
    public VoteRepository(IOptions<MongoDbSettings> databaseSettings) : base(databaseSettings)
    {
    }
}