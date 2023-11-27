using Application.EntityManagement.Products.Events;
using Domain.Abstractions;
using Domain.Common;
using Domain.Entities;
using MediatR;

namespace Application.EntityManagement.Products.Handlers;

public class ProductDeletedEventHandler : INotificationHandler<ProductDeletedEvent>
{
    private readonly IRepository<Question> _questionRepository;
    private readonly IRepository<Comment> _commentRepository;
    private readonly IRepository<Vote> _voteRepository;


    public ProductDeletedEventHandler(
        IRepository<Question> questionRepository,
        IRepository<Comment> commentRepository,
        IRepository<Vote> voteRepository)
    {
        _questionRepository = questionRepository;
        _commentRepository = commentRepository;
        _voteRepository = voteRepository;
    }

    public async Task Handle(ProductDeletedEvent notification, CancellationToken cancellationToken)
    {
        var pagination = new Pagination(1, int.MaxValue);

        var votes = (await _voteRepository.GetAllAsync(
                vote => vote.ContentId == notification.Entity.InternalId && vote.Content is Product,
                pagination,
                cancellationToken))
            .ToList();

        if (votes.Count != 0)
        {
            await _voteRepository.DeleteManyAsync(votes, cancellationToken);
        }

        var questions = (await _questionRepository.GetAllAsync(
                question => question.ProductId == notification.Entity.InternalId,
                pagination,
                cancellationToken))
            .ToList();

        if (questions.Count != 0)
        {
            await _questionRepository.DeleteManyAsync(questions, cancellationToken);
        }

        var comments = (await _commentRepository.GetAllAsync(
                comment => comment.ProductId == notification.Entity.InternalId,
                pagination,
                cancellationToken))
            .ToList();

        if (comments.Count != 0)
        {
            await _commentRepository.DeleteManyAsync(comments, cancellationToken);
        }
    }
}