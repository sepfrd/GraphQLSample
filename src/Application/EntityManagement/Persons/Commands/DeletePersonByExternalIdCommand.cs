#region

using Application.Common;
using MediatR;

#endregion

namespace Application.EntityManagement.Persons.Commands;

public record DeletePersonByExternalIdCommand(int ExternalId) : IRequest<CommandResult>;