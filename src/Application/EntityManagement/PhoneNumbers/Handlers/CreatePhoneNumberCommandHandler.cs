using Application.Abstractions;
using Application.Common.Handlers;
using Application.EntityManagement.PhoneNumbers.Dtos;
using Domain.Abstractions;
using Domain.Entities;
using Microsoft.Extensions.Logging;

namespace Application.EntityManagement.PhoneNumbers.Handlers;

public class CreatePhoneNumberCommandHandler(
        IRepository<PhoneNumber> phoneNumberRepository,
        IMappingService mappingService,
        ILogger logger)
    : BaseCreateCommandHandler<PhoneNumber, PhoneNumberDto>(phoneNumberRepository, mappingService, logger);