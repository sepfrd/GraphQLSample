using Application.Common.Commands;

namespace Application.EntityManagement.Payments.Commands;

public record DeletePaymentByExternalIdCommand(int ExternalId) : BaseDeleteByExternalIdCommand(ExternalId);