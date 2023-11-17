using Application.Common.Handlers;
using Domain.Abstractions;
using Domain.Entities;

namespace Application.EntityManagement.Addresses.Handlers;

public class GetAllAddressesQueryHandler(IRepository<Address> addressRepository)
    : BaseGetAllQueryHandler<Address>(addressRepository);