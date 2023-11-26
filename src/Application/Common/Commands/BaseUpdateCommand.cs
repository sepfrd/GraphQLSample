using MediatR;

namespace Application.Common.Commands;

public abstract record BaseUpdateCommand<TDto>(int ExternalId, TDto Dto) : IRequest<CommandResult>;