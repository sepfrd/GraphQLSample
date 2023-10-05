using MediatR;

namespace Application.Common.Commands;

public abstract record BaseCreateCommand<TDto>(TDto Dto) : IRequest<TDto?>;