using Application.Common;
using Application.EntityManagement.Users.Commands;
using Application.EntityManagement.Users.Commands.CreateUser;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;

namespace Application.EntityManagement.Users.Handlers;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, CommandResult>
{
    private readonly IRepository<User> _userRepository;
    private readonly IRepository<Person> _personRepository;

    public CreateUserCommandHandler(IUnitOfWork unitOfWork)
    {
        _userRepository = unitOfWork.UserRepository;
        _personRepository = unitOfWork.PersonRepository;
    }
    
    public async Task<CommandResult> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        return CommandResult.Success(Messages.SuccessfullyCreated);
    }
}