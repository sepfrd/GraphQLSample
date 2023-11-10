using Application.Common.Handlers;
using Domain.Abstractions;
using Domain.Entities;

namespace Application.EntityManagement.Addresses.Handlers;

public class GetAllAddressesQueryHandler : BaseGetAllQueryHandler<Address>
{
    public GetAllAddressesQueryHandler(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }
}