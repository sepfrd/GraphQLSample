using Application.Common;
using MediatR;

namespace Application.EntityManagement.Comments.Commands;

public record DeleteCommentByExternalIdCommand(int ExternalId) : IRequest<CommandResult>;