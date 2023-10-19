using Application.Common;
using Domain.Abstractions;
using Domain.Common;
using MediatR;

namespace Application.EntityManagement.Votes.Queries;

public record GetAllVotesByContentExternalIdQuery<TContent>(int ContentExternalId)
    : IRequest<QueryResponse>
    where TContent : BaseEntity, IVotableContent;