using Application.Common.Commands;

namespace Application.EntityManagement.Persons.Commands;

public record DeletePersonByExternalIdCommand(int ExternalId) : BaseDeleteByExternalIdCommand(ExternalId);