using Application.Common;
using MediatR;

namespace Application.EntityManagement.PhoneNumbers.Commands;

public record DeletePhoneNumberByExternalIdCommand(int ExternalId) : IRequest<CommandResult>;