using Application.Common.Commands;

namespace Application.EntityManagement.OrderItems.Commands;

public record DeleteOrderItemByExternalIdCommand(int ExternalId) : BaseDeleteByExternalIdCommand(ExternalId);