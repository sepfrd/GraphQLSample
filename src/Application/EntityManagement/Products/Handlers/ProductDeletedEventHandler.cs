using Application.EntityManagement.Products.Events;
using Domain.Abstractions;
using Domain.Common;
using Domain.Entities;
using MediatR;

namespace Application.EntityManagement.Products.Handlers;

public class ProductDeletedEventHandler : INotificationHandler<ProductDeletedEvent>
{
    private readonly IRepository<Answer> _answerRepository;
    private readonly IRepository<Question> _questionRepository;
    private readonly IRepository<Comment> _commentRepository;
    private readonly IRepository<Vote> _voteRepository;


    public ProductDeletedEventHandler(
        IRepository<Question> questionRepository,
        IRepository<Comment> commentRepository,
        IRepository<Vote> voteRepository,
        IRepository<Answer> answerRepository)
    {
        _questionRepository = questionRepository;
        _commentRepository = commentRepository;
        _voteRepository = voteRepository;
        _answerRepository = answerRepository;
    }

    public async Task Handle(ProductDeletedEvent notification, CancellationToken cancellationToken)
    {
        var pagination = new Pagination(1, int.MaxValue);

        var votes = (await _voteRepository.GetAllAsync(
                vote => vote.ContentId == notification.Entity.InternalId,
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

            var questionInternalIds = questions.Select(question => question.InternalId).ToList();

            var questionVotes = (await _voteRepository.GetAllAsync(
                    vote => questionInternalIds.Contains(vote.ContentId),
                    pagination,
                    cancellationToken))
                .ToList();

            if (questionVotes.Count != 0)
            {
                await _voteRepository.DeleteManyAsync(questionVotes, cancellationToken);
            }

            var questionAnswers = (await _answerRepository.GetAllAsync(
                    answer => questionInternalIds.Contains(answer.QuestionId),
                    pagination,
                    cancellationToken))
                .ToList();

            if (questionAnswers.Count != 0)
            {
                await _answerRepository.DeleteManyAsync(questionAnswers, cancellationToken);
            }
        }

        var comments = (await _commentRepository.GetAllAsync(
                comment => comment.ProductId == notification.Entity.InternalId,
                pagination,
                cancellationToken))
            .ToList();

        if (comments.Count != 0)
        {
            await _commentRepository.DeleteManyAsync(comments, cancellationToken);

            var commentInternalIds = comments.Select(comment => comment.InternalId).ToList();

            var commentVotes = (await _voteRepository.GetAllAsync(
                    vote => commentInternalIds.Contains(vote.ContentId),
                    pagination,
                    cancellationToken))
                .ToList();

            if (commentVotes.Count != 0)
            {
                await _voteRepository.DeleteManyAsync(commentVotes, cancellationToken);
            }
        }
    }
}