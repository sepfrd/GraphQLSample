using Application.Common.Handlers;
using Domain.Abstractions;
using Domain.Entities;
using Microsoft.Extensions.Logging;

namespace Application.EntityManagement.Persons.Handlers;

public class DeletePersonByExternalIdCommandHandler(IRepository<Person> personRepository, ILogger logger)
    : BaseDeleteByExternalIdCommandHandler<Person>(personRepository, logger);