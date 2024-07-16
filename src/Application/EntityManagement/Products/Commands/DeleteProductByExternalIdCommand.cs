using Application.Common;
using MediatR;

namespace Application.EntityManagement.Products.Commands;

public record DeleteProductByExternalIdCommand(int ExternalId) : IRequest<CommandResult>;