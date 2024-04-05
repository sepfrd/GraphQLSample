using Application.EntityManagement.Users.Events;
using Domain.Abstractions;
using Domain.Entities;
using Domain.Enums;
using MediatR;

namespace Application.EntityManagement.Users.Handlers;

public class UserDeletedEventHandler : INotificationHandler<UserDeletedEvent>
{
    private readonly IRepository<Address> _addressRepository;
    private readonly IRepository<Answer> _answerRepository;
    private readonly IRepository<Question> _questionRepository;
    private readonly IRepository<Comment> _commentRepository;
    private readonly IRepository<Vote> _voteRepository;
    private readonly IRepository<Person> _personRepository;
    private readonly IRepository<Cart> _cartRepository;
    private readonly IRepository<CartItem> _cartItemRepository;
    private readonly IRepository<Order> _orderRepository;
    private readonly IRepository<OrderItem> _orderItemRepository;
    private readonly IRepository<PhoneNumber> _phoneNumberRepository;
    private readonly IRepository<UserRole> _userRoleRepository;

    public UserDeletedEventHandler(
        IRepository<Address> addressRepository,
        IRepository<Answer> answerRepository,
        IRepository<Question> questionRepository,
        IRepository<Comment> commentRepository,
        IRepository<Vote> voteRepository,
        IRepository<Person> personRepository,
        IRepository<Cart> cartRepository,
        IRepository<CartItem> cartItemRepository,
        IRepository<Order> orderRepository,
        IRepository<OrderItem> orderItemRepository,
        IRepository<PhoneNumber> phoneNumberRepository,
        IRepository<UserRole> userRoleRepository)
    {
        _answerRepository = answerRepository;
        _questionRepository = questionRepository;
        _commentRepository = commentRepository;
        _voteRepository = voteRepository;
        _personRepository = personRepository;
        _cartRepository = cartRepository;
        _orderRepository = orderRepository;
        _orderItemRepository = orderItemRepository;
        _cartItemRepository = cartItemRepository;
        _phoneNumberRepository = phoneNumberRepository;
        _userRoleRepository = userRoleRepository;
        _addressRepository = addressRepository;
    }

    public async Task Handle(UserDeletedEvent notification, CancellationToken cancellationToken)
    {
        var votes = (await _voteRepository.GetAllAsync(
                vote => vote.UserId == notification.Entity.InternalId,
                cancellationToken))
            .ToList();

        if (votes.Count != 0)
        {
            await _voteRepository.DeleteManyAsync(votes, cancellationToken);
        }

        var answers = (await _answerRepository.GetAllAsync(
                answer => answer.UserId == notification.Entity.InternalId,
                cancellationToken))
            .ToList();

        if (answers.Count != 0)
        {
            await _answerRepository.DeleteManyAsync(answers, cancellationToken);
        }

        var answerInternalIds = answers.Select(answer => answer.InternalId).ToList();

        var answerVotes = (await _voteRepository.GetAllAsync(
                vote => answerInternalIds.Contains(vote.ContentId) &&
                        vote.ContentType == VotableContentType.Answer,
                cancellationToken))
            .ToList();

        if (answerVotes.Count != 0)
        {
            await _voteRepository.DeleteManyAsync(answerVotes, cancellationToken);
        }

        var userRoles = (await _userRoleRepository.GetAllAsync(
                userRole => userRole.UserId == notification.Entity.InternalId,
                cancellationToken))
            .ToList();

        if (userRoles.Count != 0)
        {
            await _userRoleRepository.DeleteManyAsync(userRoles, cancellationToken);
        }

        var questions = (await _questionRepository.GetAllAsync(
                question => question.UserId == notification.Entity.InternalId,
                cancellationToken))
            .ToList();

        if (questions.Count != 0)
        {
            await _questionRepository.DeleteManyAsync(questions, cancellationToken);

            var questionInternalIds = questions.Select(question => question.InternalId).ToList();

            var questionVotes = (await _voteRepository.GetAllAsync(
                    vote => questionInternalIds.Contains(vote.ContentId) &&
                            vote.ContentType == VotableContentType.Question,
                    cancellationToken))
                .ToList();

            if (questionVotes.Count != 0)
            {
                await _voteRepository.DeleteManyAsync(questionVotes, cancellationToken);
            }

            var questionAnswers = (await _answerRepository.GetAllAsync(
                    answer => questionInternalIds.Contains(answer.QuestionId),
                    cancellationToken))
                .ToList();

            if (questionAnswers.Count != 0)
            {
                await _answerRepository.DeleteManyAsync(questionAnswers, cancellationToken);
            }

            var questionAnswersInternalIds =
                questionAnswers.Select(questionAnswer => questionAnswer.InternalId).ToList();

            var questionAnswersVotes = (await _voteRepository.GetAllAsync(
                    vote => questionAnswersInternalIds.Contains(vote.ContentId) &&
                            vote.ContentType == VotableContentType.Answer,
                    cancellationToken))
                .ToList();

            if (questionAnswersVotes.Count != 0)
            {
                await _voteRepository.DeleteManyAsync(questionAnswersVotes, cancellationToken);
            }
        }

        var comments = (await _commentRepository.GetAllAsync(
                comment => comment.UserId == notification.Entity.InternalId,
                cancellationToken))
            .ToList();

        if (comments.Count != 0)
        {
            await _commentRepository.DeleteManyAsync(comments, cancellationToken);

            var commentInternalIds = comments.Select(comment => comment.InternalId).ToList();

            var commentVotes = (await _voteRepository.GetAllAsync(
                    vote => commentInternalIds.Contains(vote.ContentId) &&
                            vote.ContentType == VotableContentType.Comment,
                    cancellationToken))
                .ToList();

            if (commentVotes.Count != 0)
            {
                await _voteRepository.DeleteManyAsync(commentVotes, cancellationToken);
            }
        }

        var phoneNumbers = (await _phoneNumberRepository.GetAllAsync(
                phoneNumber => phoneNumber.UserId == notification.Entity.InternalId,
                cancellationToken))
            .ToList();

        if (phoneNumbers.Count != 0)
        {
            await _phoneNumberRepository.DeleteManyAsync(phoneNumbers, cancellationToken);
        }

        var addresses = (await _addressRepository.GetAllAsync(
                address => address.UserId == notification.Entity.InternalId,
                cancellationToken))
            .ToList();

        if (addresses.Count != 0)
        {
            await _addressRepository.DeleteManyAsync(addresses, cancellationToken);
        }

        var orders = (await _orderRepository.GetAllAsync(
                order => order.UserId == notification.Entity.InternalId,
                cancellationToken))
            .ToList();

        if (orders.Count != 0)
        {
            await _orderRepository.DeleteManyAsync(orders, cancellationToken);

            var orderInternalIds = orders.Select(order => order.InternalId).ToList();

            var orderItems = (await _orderItemRepository.GetAllAsync(
                    orderItem => orderInternalIds.Contains(orderItem.OrderId),
                    cancellationToken))
                .ToList();

            if (orderItems.Count != 0)
            {
                await _orderItemRepository.DeleteManyAsync(orderItems, cancellationToken);
            }
        }

        var person = await _personRepository.GetByInternalIdAsync(notification.Entity.PersonId, cancellationToken);

        if (person is not null)
        {
            await _personRepository.DeleteOneAsync(person, cancellationToken);
        }

        var cart = await _cartRepository.GetByInternalIdAsync(notification.Entity.CartId, cancellationToken);

        if (cart is not null)
        {
            await _cartRepository.DeleteOneAsync(cart, cancellationToken);

            var cartItems = (await _cartItemRepository.GetAllAsync(
                    cartItem => cartItem.CartId == cart.InternalId,
                    cancellationToken))
                .ToList();

            if (cartItems.Count != 0)
            {
                await _cartItemRepository.DeleteManyAsync(cartItems, cancellationToken);
            }
        }
    }
}