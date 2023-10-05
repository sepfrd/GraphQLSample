using MediatR;

namespace Application.Common.Commands;

public abstract record BaseDeleteByExternalIdCommand<TDto>(int Id) : IRequest<TDto?>;