using Application.Common;
using MediatR;

namespace Application.EntityManagement.Answers.Commands;

public record DeleteAnswerByInternalIdCommand(Guid InternalId) : IRequest<CommandResult>;