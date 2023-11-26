#region

using Application.Common;
using Application.EntityManagement.Answers.Dtos;
using MediatR;

#endregion

namespace Application.EntityManagement.Answers.Commands;

public record UpdateAnswerCommand(int ExternalId, AnswerDto AnswerDto) : IRequest<CommandResult>;