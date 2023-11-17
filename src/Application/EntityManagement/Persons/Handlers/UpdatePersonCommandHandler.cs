using Application.Abstractions;
using Application.Common.Handlers;
using Application.EntityManagement.Persons.Dtos;
using Domain.Abstractions;
using Domain.Entities;
using Microsoft.Extensions.Logging;

namespace Application.EntityManagement.Persons.Handlers;

public class UpdatePersonCommandHandler(
        IRepository<Person> personRepository,
        IMappingService mappingService,
        ILogger logger)
    : BaseUpdateCommandHandler<Person, PersonDto>(personRepository, mappingService, logger);