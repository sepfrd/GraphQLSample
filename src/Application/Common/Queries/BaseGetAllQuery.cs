using MediatR;

namespace Application.Common.Queries;

public abstract record BaseGetAllQuery : IRequest<QueryResponse>;