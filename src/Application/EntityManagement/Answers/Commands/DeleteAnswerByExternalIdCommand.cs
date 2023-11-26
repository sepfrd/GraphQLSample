#region

using Application.Common;
using MediatR;

#endregion

namespace Application.EntityManagement.Answers.Commands;

public record DeleteAnswerByExternalIdCommand(int ExternalId) : IRequest<CommandResult>;