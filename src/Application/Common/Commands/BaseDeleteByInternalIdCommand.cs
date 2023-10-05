using MediatR;

namespace Application.Common.Commands;

public abstract record BaseDeleteByInternalIdCommand<TDto>(Guid Id) : IRequest<TDto?>;