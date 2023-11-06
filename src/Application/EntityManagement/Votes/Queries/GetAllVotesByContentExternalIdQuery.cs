using Application.Common;
using Application.EntityManagement.Votes.Dtos;
using Domain.Abstractions;
using Domain.Common;
using MediatR;

namespace Application.EntityManagement.Votes.Queries;

public record GetAllVotesByContentExternalIdQuery(int ContentExternalId)
    : IRequest<QueryReferenceResponse<GetAllVotesByContentExternalIdResponseDto>>;