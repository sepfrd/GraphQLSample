#region

using Application.Common;
using Application.EntityManagement.Persons.Dtos;
using MediatR;

#endregion

namespace Application.EntityManagement.Persons.Commands;

public record UpdatePersonCommand(int ExternalId, PersonDto PersonDto) : IRequest<CommandResult>;