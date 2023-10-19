using Application.Abstractions;
using Application.Common.Handlers;
using Application.EntityManagement.Addresses.Dtos;
using Domain.Abstractions;
using Domain.Entities;
using Microsoft.Extensions.Logging;

namespace Application.EntityManagement.Addresses.Handlers;

public class UpdateAddressCommandHandler : BaseUpdateCommandHandler<Address, AddressDto>
{
    public UpdateAddressCommandHandler(IUnitOfWork unitOfWork, IMappingService mappingService, ILogger logger) : base(unitOfWork, mappingService, logger)
    {
    }
}