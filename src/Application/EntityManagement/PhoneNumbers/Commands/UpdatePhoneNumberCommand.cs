using Application.Common;
using Application.EntityManagement.PhoneNumbers.Dtos;
using MediatR;

namespace Application.EntityManagement.PhoneNumbers.Commands;

public record UpdatePhoneNumberCommand(int ExternalId, PhoneNumberDto PhoneNumberDto) : IRequest<CommandResult>;