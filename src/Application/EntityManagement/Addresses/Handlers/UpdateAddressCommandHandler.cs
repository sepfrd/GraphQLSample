using Application.Abstractions;
using Application.Common.Handlers;
using Application.EntityManagement.Addresses.Dtos;
using Domain.Abstractions;
using Domain.Entities;
using Microsoft.Extensions.Logging;

namespace Application.EntityManagement.Addresses.Handlers;

public class UpdateAddressCommandHandler(
        IRepository<Address> addressRepository,
        IMappingService mappingService,
        ILogger logger)
    : BaseUpdateCommandHandler<Address, AddressDto>(addressRepository, mappingService, logger);