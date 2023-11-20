using Application.Common;
using MediatR;

namespace Application.EntityManagement.Persons.Commands;

public record DeletePersonByExternalIdCommand(int ExternalId) : IRequest<CommandResult>;