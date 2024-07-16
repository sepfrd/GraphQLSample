using Application.Common;
using Application.EntityManagement.Persons.Dtos.PersonDto;
using MediatR;

namespace Application.EntityManagement.Persons.Commands;

public record UpdatePersonCommand(int ExternalId, PersonDto PersonDto) : IRequest<CommandResult>;