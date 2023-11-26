using Application.Common;
using Application.EntityManagement.PhoneNumbers.Dtos;
using MediatR;

namespace Application.EntityManagement.PhoneNumbers.Commands;

public record CreatePhoneNumberCommand(PhoneNumberDto PhoneNumberDto) : IRequest<CommandResult>;