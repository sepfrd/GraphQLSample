using Application.Common.Handlers;
using Domain.Abstractions;
using Domain.Entities;
using Microsoft.Extensions.Logging;

namespace Application.EntityManagement.Persons.Handlers;

public class DeletePersonByExternalIdCommandHandler : BaseDeleteByExternalIdCommandHandler<Person>
{
    public DeletePersonByExternalIdCommandHandler(IUnitOfWork unitOfWork, ILogger logger) : base(unitOfWork, logger)
    {
    }
}