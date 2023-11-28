using Application.EntityManagement.Comments.Events;
using Domain.Abstractions;
using Domain.Common;
using Domain.Entities;
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
        var pagination = new Pagination(1, int.MaxValue);

        var votes = (await _repository.GetAllAsync(
                vote => vote.ContentId == notification.Entity.InternalId && vote.Content is Comment,
                pagination,
                cancellationToken))
            .ToList();

        if (votes.Count != 0)
        {
            await _repository.DeleteManyAsync(votes, cancellationToken);
        }
    }
}