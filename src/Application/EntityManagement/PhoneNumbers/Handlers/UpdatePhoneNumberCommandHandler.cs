using Application.Abstractions;
using Application.Common.Handlers;
using Application.EntityManagement.PhoneNumbers.Dtos;
using Domain.Abstractions;
using Domain.Entities;
using Microsoft.Extensions.Logging;

namespace Application.EntityManagement.PhoneNumbers.Handlers;

public class UpdatePhoneNumberCommandHandler : BaseUpdateCommandHandler<PhoneNumber, PhoneNumberDto>
{
    public UpdatePhoneNumberCommandHandler(IUnitOfWork unitOfWork, IMappingService mappingService, ILogger logger) : base(unitOfWork, mappingService, logger)
    {
    }
}