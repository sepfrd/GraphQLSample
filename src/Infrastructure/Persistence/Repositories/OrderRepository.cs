using Domain.Entities;
using Infrastructure.Common.Configurations;
using Infrastructure.Persistence.Common;
using Microsoft.Extensions.Options;

namespace Infrastructure.Persistence.Repositories;

public class OrderRepository(IOptions<AppOptions> appOptions) : BaseRepository<Order>(appOptions);