using Domain.Entities;
using Infrastructure.Persistence.Common;
using Microsoft.Extensions.Options;

namespace Infrastructure.Persistence.Repositories;

public class PhoneNumberRepository(IOptions<MongoDbSettings> databaseSettings)
    : BaseRepository<PhoneNumber>(databaseSettings);