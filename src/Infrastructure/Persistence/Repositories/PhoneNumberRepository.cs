using Domain.Entities;
using Infrastructure.Persistence.Common;
using Microsoft.Extensions.Options;

namespace Infrastructure.Persistence.Repositories;

public class PhoneNumberRepository : BaseRepository<PhoneNumber>
{
    public PhoneNumberRepository(IOptions<MongoDbSettings> databaseSettings) : base(databaseSettings)
    {
    }
}