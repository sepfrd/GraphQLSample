using Application.Common;
using MediatR;

namespace Application.EntityManagement.Persons.Commands;

public record DeletePersonByInternalIdCommand(Guid InternalId) : IRequest<CommandResult>;