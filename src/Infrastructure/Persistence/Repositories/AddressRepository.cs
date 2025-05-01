using Domain.Entities;
using Infrastructure.Common.Configurations;
using Infrastructure.Persistence.Common;
using Microsoft.Extensions.Options;

namespace Infrastructure.Persistence.Repositories;

public class AddressRepository(IOptions<AppOptions> appOptions) : BaseRepository<Address>(appOptions);