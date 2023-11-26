#region

using Application.Common;
using MediatR;

#endregion

namespace Application.EntityManagement.Products.Commands;

public record DeleteProductByExternalIdCommand(int ExternalId) : IRequest<CommandResult>;