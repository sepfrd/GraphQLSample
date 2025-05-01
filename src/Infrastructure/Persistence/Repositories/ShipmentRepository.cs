using Domain.Entities;
using Infrastructure.Common.Configurations;
using Infrastructure.Persistence.Common;
using Microsoft.Extensions.Options;

namespace Infrastructure.Persistence.Repositories;

public class ShipmentRepository(IOptions<AppOptions> appOptions)
    : BaseRepository<Shipment>(appOptions);