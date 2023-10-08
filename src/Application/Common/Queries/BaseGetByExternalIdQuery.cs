using MediatR;

namespace Application.Common.Queries;

public abstract record BaseGetByExternalIdQuery(int ExternalId) : IRequest<QueryResponse>;