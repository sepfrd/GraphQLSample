using Application.Common.Handlers;
using Domain.Abstractions;
using Domain.Entities;
using Microsoft.Extensions.Logging;

namespace Application.EntityManagement.Addresses.Handlers;

public class DeleteAddressByExternalIdCommandHandler : BaseDeleteByExternalIdCommandHandler<Address>
{
    public DeleteAddressByExternalIdCommandHandler(IUnitOfWork unitOfWork, ILogger logger) : base(unitOfWork, logger)
    {
    }
}