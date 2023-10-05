using MediatR;

namespace Application.Common.Queries;

public abstract record BaseGetByExternalIdQuery<TDto>(int Id) : IRequest<TDto?>;