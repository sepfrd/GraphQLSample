using Application.Common.Handlers;
using Domain.Abstractions;
using Domain.Entities;
using Microsoft.Extensions.Logging;

namespace Application.EntityManagement.PhoneNumbers.Handlers;

public class DeletePhoneNumberByExternalIdCommandHandler : BaseDeleteByExternalIdCommandHandler<PhoneNumber>
{
    public DeletePhoneNumberByExternalIdCommandHandler(IUnitOfWork unitOfWork, ILogger logger) : base(unitOfWork, logger)
    {
    }
}