using Domain.Entities;
using Infrastructure.Common.Configurations;
using Infrastructure.Persistence.Common;
using Microsoft.Extensions.Options;

namespace Infrastructure.Persistence.Repositories;

public class PersonRepository(IOptions<AppOptions> appOptions) : BaseRepository<Person>(appOptions);