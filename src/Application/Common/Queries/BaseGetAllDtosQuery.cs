using MediatR;

namespace Application.Common.Queries;

public abstract record BaseGetAllDtosQuery : IRequest<QueryResponse>;