using MediatR;

namespace Application.Common.Commands;

public abstract record BaseDeleteByInternalIdCommand(Guid Id) : IRequest<CommandResult>;