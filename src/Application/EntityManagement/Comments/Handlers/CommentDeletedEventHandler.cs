using Application.EntityManagement.Comments.Events;
using Domain.Abstractions;
using Domain.Common;
using Domain.Entities;
using Domain.Enums;
using MediatR;

namespace Application.EntityManagement.Comments.Handlers;

public class CommentDeletedEventHandler : INotificationHandler<CommentDeletedEvent>
{
    private readonly IRepository<Vote> _repository;

    public CommentDeletedEventHandler(IRepository<Vote> repository)
    {
        _repository = repository;
    }

    public async Task Handle(CommentDeletedEvent notification, CancellationToken cancellationToken)
    {
        var votes = (await _repository.GetAllAsync(
                vote => vote.ContentId == notification.Entity.InternalId &&
                        vote.ContentType == VotableContentType.Comment,
                Pagination.MaxPagination,
                cancellationToken))
            .ToList();

        if (votes.Count != 0)
        {
            await _repository.DeleteManyAsync(votes, cancellationToken);
        }
    }
}