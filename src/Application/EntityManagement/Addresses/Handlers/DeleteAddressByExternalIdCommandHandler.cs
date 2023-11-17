using Application.Common.Handlers;
using Domain.Abstractions;
using Domain.Entities;
using Microsoft.Extensions.Logging;

namespace Application.EntityManagement.Addresses.Handlers;

public class DeleteAddressByExternalIdCommandHandler(IRepository<Address> addressRepository, ILogger logger)
    : BaseDeleteByExternalIdCommandHandler<Address>(addressRepository, logger);