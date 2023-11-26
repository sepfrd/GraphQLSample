#region

using Application.Common;
using MediatR;

#endregion

namespace Application.EntityManagement.Questions.Commands;

public record DeleteQuestionByExternalIdCommand(int ExternalId) : IRequest<CommandResult>;