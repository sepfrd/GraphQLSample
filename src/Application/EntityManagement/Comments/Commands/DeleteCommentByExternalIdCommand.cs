#region

using Application.Common;
using MediatR;

#endregion

namespace Application.EntityManagement.Comments.Commands;

public record DeleteCommentByExternalIdCommand(int ExternalId) : IRequest<CommandResult>;