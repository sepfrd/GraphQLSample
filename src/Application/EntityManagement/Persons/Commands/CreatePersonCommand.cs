using Application.Common;
using Application.EntityManagement.Persons.Dtos;
using MediatR;

namespace Application.EntityManagement.Persons.Commands;

public record CreatePersonCommand(PersonDto PersonDto) : IRequest<CommandResult>;