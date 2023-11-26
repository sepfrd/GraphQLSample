#region

using Application.Common;
using MediatR;

#endregion

namespace Application.EntityManagement.Persons.Commands;

public record DeletePersonByInternalIdCommand(Guid InternalId) : IRequest<CommandResult>;