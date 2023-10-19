using Application.Common.Commands;

namespace Application.EntityManagement.PhoneNumbers.Commands;

public record DeletePhoneNumberByExternalIdCommand(int ExternalId) : BaseDeleteByExternalIdCommand(ExternalId);