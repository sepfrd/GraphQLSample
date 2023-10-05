using MediatR;

namespace Application.Common.Commands;

public abstract record BaseUpdateCommand<TDto>(TDto Dto) : IRequest<TDto?>;