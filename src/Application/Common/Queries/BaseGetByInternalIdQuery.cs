using MediatR;

namespace Application.Common.Queries;

public abstract record BaseGetByInternalIdQuery(Guid Id)
    : IRequest<QueryResponse>;