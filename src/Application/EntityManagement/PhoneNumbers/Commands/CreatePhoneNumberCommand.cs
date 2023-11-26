#region

using Application.Common;
using Application.EntityManagement.PhoneNumbers.Dtos;
using MediatR;

#endregion

namespace Application.EntityManagement.PhoneNumbers.Commands;

public record CreatePhoneNumberCommand(PhoneNumberDto PhoneNumberDto) : IRequest<CommandResult>;