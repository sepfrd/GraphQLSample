using Application.Abstractions;
using Application.Common.Handlers;
using Application.EntityManagement.PhoneNumbers.Dtos;
using Domain.Abstractions;
using Domain.Entities;
using Microsoft.Extensions.Logging;

namespace Application.EntityManagement.PhoneNumbers.Handlers;

public class CreatePhoneNumberCommandHandler : BaseCreateCommandHandler<PhoneNumber, PhoneNumberDto>
{
    public CreatePhoneNumberCommandHandler(IUnitOfWork unitOfWork, IMappingService mappingService, ILogger logger) : base(unitOfWork, mappingService, logger)
    {
    }
}