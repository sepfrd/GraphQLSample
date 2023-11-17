using Application.Common.Commands;

namespace Application.EntityManagement.Products.Commands;

public record DeleteProductByExternalIdCommand(int ExternalId) : BaseDeleteByExternalIdCommand(ExternalId);