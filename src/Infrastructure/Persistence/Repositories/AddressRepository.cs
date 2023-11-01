using Domain.Entities;
using Infrastructure.Persistence.Common;
using Microsoft.Extensions.Options;

namespace Infrastructure.Persistence.Repositories;

public class AddressRepository : BaseRepository<Address>
{
    public AddressRepository(IOptions<MongoDbSettings> databaseSettings) : base(databaseSettings)
    {
    }
}