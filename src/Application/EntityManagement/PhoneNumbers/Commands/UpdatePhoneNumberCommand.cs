using Application.Common;
using Application.EntityManagement.PhoneNumbers.Dtos;
using Application.EntityManagement.PhoneNumbers.Dtos.PhoneNumberDto;
using MediatR;

namespace Application.EntityManagement.PhoneNumbers.Commands;

public record UpdatePhoneNumberCommand(int ExternalId, PhoneNumberDto PhoneNumberDto) : IRequest<CommandResult>;