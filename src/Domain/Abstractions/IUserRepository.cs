using Domain.Entities;

namespace Domain.Abstractions;

public interface IUserRepository : IRepository<User>
{
    Task<IEnumerable<PhoneNumber>?> GetAllPhoneNumbersByInternalIdsAsync(ICollection<Guid>? phoneNumberIds, CancellationToken cancellationToken = default);

    Task<IEnumerable<Address>?> GetAllAddressesByInternalIdsAsync(ICollection<Guid>? addresses, CancellationToken cancellationToken = default);
}