using Application.Common;
using MediatR;

namespace Application.EntityManagement.Answers.Commands;

public record DeleteAnswerByExternalIdCommand(int ExternalId) : IRequest<CommandResult>;