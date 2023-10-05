using MediatR;

namespace Application.Common.Queries;

public abstract record BaseGetAllDtosQuery<TDto> : IRequest<IEnumerable<TDto>?>;