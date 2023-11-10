using Application.Abstractions;
using Application.Common.Handlers;
using Application.EntityManagement.Persons.Dtos;
using Domain.Abstractions;
using Domain.Entities;
using Microsoft.Extensions.Logging;

namespace Application.EntityManagement.Persons.Handlers;

public class UpdatePersonCommandHandler : BaseUpdateCommandHandler<Person, PersonDto>
{
    public UpdatePersonCommandHandler(IUnitOfWork unitOfWork, IMappingService mappingService, ILogger logger) : base(unitOfWork, mappingService, logger)
    {
    }
}