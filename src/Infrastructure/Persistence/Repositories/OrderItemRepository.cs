using Domain.Entities;
using Infrastructure.Common.Configurations;
using Infrastructure.Persistence.Common;
using Microsoft.Extensions.Options;

namespace Infrastructure.Persistence.Repositories;

public class OrderItemRepository(IOptions<AppOptions> appOptions)
    : BaseRepository<OrderItem>(appOptions);