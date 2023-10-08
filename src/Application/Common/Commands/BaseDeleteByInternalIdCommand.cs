using MediatR;

namespace Application.Common.Commands;

public abstract record BaseDeleteByInternalIdCommand(Guid InternalId) : IRequest<CommandResult>;