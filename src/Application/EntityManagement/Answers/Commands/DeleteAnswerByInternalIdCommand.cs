#region

using Application.Common;
using MediatR;

#endregion

namespace Application.EntityManagement.Answers.Commands;

public record DeleteAnswerByInternalIdCommand(Guid InternalId) : IRequest<CommandResult>;