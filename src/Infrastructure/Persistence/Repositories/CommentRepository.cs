using Domain.Entities;
using Infrastructure.Persistence.Common;
using Microsoft.Extensions.Options;

namespace Infrastructure.Persistence.Repositories;

public class CommentRepository : BaseRepository<Comment>
{
    public CommentRepository(IOptions<MongoDbSettings> databaseSettings) : base(databaseSettings)
    {
    }
}