using Application.Common;
using Application.EntityManagement.Persons.Dtos;
using MediatR;

namespace Application.EntityManagement.Persons.Commands;

public record UpdatePersonCommand(int ExternalId, UpdatePersonDto UpdatePersonDto) : IRequest<CommandResult>;