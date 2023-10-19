using Application.Common.Commands;

namespace Application.EntityManagement.Orders.Commands;

public record DeleteOrderByExternalIdCommand(int ExternalId) : BaseDeleteByExternalIdCommand(ExternalId);