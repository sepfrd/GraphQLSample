using Application.EntityManagement.Users.Events;
using Domain.Abstractions;
using Domain.Common;
using Domain.Entities;
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
    private readonly IRepository<Order> _orderRepository;
    private readonly IRepository<PhoneNumber> _phoneNumberRepository;
    private readonly IRepository<UserRole> _userRoleRepository;

    public UserDeletedEventHandler(
        IRepository<Answer> answerRepository,
        IRepository<Question> questionRepository,
        IRepository<Comment> commentRepository,
        IRepository<Vote> voteRepository,
        IRepository<Person> personRepository,
        IRepository<Cart> cartRepository,
        IRepository<Order> orderRepository,
        IRepository<PhoneNumber> phoneNumberRepository,
        IRepository<UserRole> userRoleRepository,
        IRepository<Address> addressRepository)
    {
        _answerRepository = answerRepository;
        _questionRepository = questionRepository;
        _commentRepository = commentRepository;
        _voteRepository = voteRepository;
        _personRepository = personRepository;
        _cartRepository = cartRepository;
        _orderRepository = orderRepository;
        _phoneNumberRepository = phoneNumberRepository;
        _userRoleRepository = userRoleRepository;
        _addressRepository = addressRepository;
    }

    public async Task Handle(UserDeletedEvent notification, CancellationToken cancellationToken)
    {
        var pagination = new Pagination(1, int.MaxValue);

        var votes = (await _voteRepository.GetAllAsync(
                vote => vote.UserId == notification.Entity.InternalId,
                pagination,
                cancellationToken))
            .ToList();

        if (votes.Count != 0)
        {
            await _voteRepository.DeleteManyAsync(votes, cancellationToken);
        }

        var answers = (await _answerRepository.GetAllAsync(
                answer => answer.UserId == notification.Entity.InternalId,
                pagination,
                cancellationToken))
            .ToList();

        if (answers.Count != 0)
        {
            await _answerRepository.DeleteManyAsync(answers, cancellationToken);
        }

        var userRoles = (await _userRoleRepository.GetAllAsync(
                userRole => userRole.UserId == notification.Entity.InternalId,
                pagination,
                cancellationToken))
            .ToList();

        if (userRoles.Count != 0)
        {
            await _userRoleRepository.DeleteManyAsync(userRoles, cancellationToken);
        }

        var questions = (await _questionRepository.GetAllAsync(
                question => question.UserId == notification.Entity.InternalId,
                pagination,
                cancellationToken))
            .ToList();

        if (questions.Count != 0)
        {
            await _questionRepository.DeleteManyAsync(questions, cancellationToken);
        }

        var comments = (await _commentRepository.GetAllAsync(
                comment => comment.UserId == notification.Entity.InternalId,
                pagination,
                cancellationToken))
            .ToList();

        if (comments.Count != 0)
        {
            await _commentRepository.DeleteManyAsync(comments, cancellationToken);
        }

        var phoneNumbers = (await _phoneNumberRepository.GetAllAsync(
                phoneNumber => phoneNumber.UserId == notification.Entity.InternalId,
                pagination,
                cancellationToken))
            .ToList();

        if (phoneNumbers.Count != 0)
        {
            await _phoneNumberRepository.DeleteManyAsync(phoneNumbers, cancellationToken);
        }

        var addresses = (await _addressRepository.GetAllAsync(
                address => address.UserId == notification.Entity.InternalId,
                pagination,
                cancellationToken))
            .ToList();

        if (addresses.Count != 0)
        {
            await _addressRepository.DeleteManyAsync(addresses, cancellationToken);
        }

        var orders = (await _orderRepository.GetAllAsync(
                order => order.UserId == notification.Entity.InternalId,
                pagination,
                cancellationToken))
            .ToList();

        if (orders.Count != 0)
        {
            await _orderRepository.DeleteManyAsync(orders, cancellationToken);
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
        }
    }
}

//
// public Person? Person { get; set; }
//
//
// public Cart? Cart { get; set; }
//
//