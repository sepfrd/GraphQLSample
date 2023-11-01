using Domain.Entities;
using Infrastructure.Persistence.Common;
using Microsoft.Extensions.Options;

namespace Infrastructure.Persistence.Repositories;

public class PersonRepository : BaseRepository<Person>
{
    public PersonRepository(IOptions<MongoDbSettings> databaseSettings) : base(databaseSettings)
    {
    }
}