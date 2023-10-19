using Application.Common;
using Application.EntityManagement.Votes.Dtos;
using Application.EntityManagement.Votes.Queries;
using Domain.Abstractions;
using Domain.Common;
using Domain.Enums;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Application.EntityManagement.Votes.Handlers;

public class GetAllVotesByContentExternalIdQueryHandler<TContent>
    : IRequestHandler<GetAllVotesByContentExternalIdQuery<TContent>, QueryResponse>
    where TContent : BaseEntity, IVotableContent
{
    private readonly ILogger _logger;
    private readonly IRepository<TContent> _repository;

    public GetAllVotesByContentExternalIdQueryHandler(IUnitOfWork unitOfWork, ILogger logger)
    {
        _logger = logger;

        var repositoryInterface = unitOfWork
            .Repositories
            .First(repository => repository is IRepository<TContent>);

        _repository = (IRepository<TContent>)repositoryInterface;
    }

    public async Task<QueryResponse> Handle(GetAllVotesByContentExternalIdQuery<TContent> request, CancellationToken cancellationToken)
    {
        var content = await _repository.GetByExternalIdAsync(request.ContentExternalId,
            new Func<TContent, object?>[]
            {
                entity => entity.Votes
            },
            cancellationToken);

        if (content is null)
        {
            return new QueryResponse
                (
                null,
                false,
                Messages.NotFound,
                HttpStatusCode.NotFound
                );
        }

        if (content.Votes is null)
        {
            _logger.LogError(Messages.EntityRelationshipsRetrievalFailed, DateTime.UtcNow, typeof(TContent), typeof(GetAllVotesByContentExternalIdQueryHandler<TContent>));

            return new QueryResponse
                (
                null,
                false,
                Messages.InternalServerError,
                HttpStatusCode.InternalServerError
                );
        }

        var hatesCount = content.Votes.Count(vote => vote.Type is VoteType.Hate);
        var dislikesCount = content.Votes.Count(vote => vote.Type is VoteType.Dislike);
        var likesCount = content.Votes.Count(vote => vote.Type is VoteType.Like);
        var superLikesCount = content.Votes.Count(vote => vote.Type is VoteType.SuperLike);

        var response = new GetAllVotesByContentExternalIdResponseDto
            (
            hatesCount,
            dislikesCount,
            likesCount,
            superLikesCount
            );

        return new QueryResponse
            (
            response,
            true,
            Messages.SuccessfullyRetrieved,
            HttpStatusCode.OK
            );
    }
}