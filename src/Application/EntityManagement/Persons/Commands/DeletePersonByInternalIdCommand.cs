using Application.Common.Commands;

namespace Application.EntityManagement.Persons.Commands;

public record DeletePersonByInternalIdCommand(Guid InternalId) : BaseDeleteByInternalIdCommand(InternalId);