using MediatR;

namespace Application.Common.Commands;

public abstract record BaseDeleteByExternalIdCommand(int ExternalId) : IRequest<CommandResult>;