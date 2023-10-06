using MediatR;

namespace Application.Common.Commands;

public abstract record BaseUpdateCommand<TDto>(int Id, TDto Dto) : IRequest<CommandResult>;