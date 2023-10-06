using MediatR;

namespace Application.Common.Queries;

public abstract record BaseGetByExternalIdQuery(int Id) : IRequest<QueryResponse>;