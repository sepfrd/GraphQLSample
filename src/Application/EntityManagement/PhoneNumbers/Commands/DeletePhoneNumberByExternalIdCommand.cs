#region

using Application.Common;
using MediatR;

#endregion

namespace Application.EntityManagement.PhoneNumbers.Commands;

public record DeletePhoneNumberByExternalIdCommand(int ExternalId) : IRequest<CommandResult>;