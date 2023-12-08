using Application.Abstractions;
using Application.Common;
using Application.EntityManagement.Users.Commands;
using Domain.Abstractions;
using Domain.Common;
using Domain.Entities;
using MediatR;

namespace Application.EntityManagement.Users.Handlers;

public class LoginCommandHandler : IRequestHandler<LoginCommand, CommandResult>
{
    private readonly IRepository<User> _userRepository;
    private readonly IAuthenticationService _authenticationService;

    public LoginCommandHandler(IRepository<User> userRepository, IAuthenticationService authenticationService)
    {
        _userRepository = userRepository;
        _authenticationService = authenticationService;
    }

    public async Task<CommandResult> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var users = await _userRepository.GetAllAsync(
            user => user.Username == request.LoginDto.UsernameOrEmail || user.Email == request.LoginDto.UsernameOrEmail,
            new Pagination(),
            cancellationToken);

        var user = users.FirstOrDefault();

        if (user is null)
        {
            return CommandResult.Failure(MessageConstants.InvalidCredentials);
        }

        var isPasswordValid = request.LoginDto.Password == user.Password;

        if (!isPasswordValid)
        {
            return CommandResult.Failure(MessageConstants.InvalidCredentials);
        }

        var jwt = await _authenticationService.CreateJwtAsync(user, cancellationToken);

        return CommandResult.Success(MessageConstants.SuccessfullyLoggedIn, jwt);
    }
}