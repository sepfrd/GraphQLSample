using Domain.Entities;
using Infrastructure.Persistence.Common;
using Microsoft.Extensions.Options;

namespace Infrastructure.Persistence.Repositories;

public class PaymentRepository : BaseRepository<Payment>
{
    public PaymentRepository(IOptions<MongoDbSettings> databaseSettings) : base(databaseSettings)
    {
    }
}