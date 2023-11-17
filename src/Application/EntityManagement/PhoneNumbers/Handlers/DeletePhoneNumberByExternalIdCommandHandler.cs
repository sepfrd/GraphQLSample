using Application.Common.Handlers;
using Domain.Abstractions;
using Domain.Entities;
using Microsoft.Extensions.Logging;

namespace Application.EntityManagement.PhoneNumbers.Handlers;

public class DeletePhoneNumberByExternalIdCommandHandler(
        IRepository<PhoneNumber> phoneNumberRepository,
        ILogger logger)
    : BaseDeleteByExternalIdCommandHandler<PhoneNumber>(phoneNumberRepository, logger);