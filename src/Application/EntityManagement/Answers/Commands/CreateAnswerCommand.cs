#region

using Application.Common;
using Application.EntityManagement.Answers.Dtos;
using MediatR;

#endregion

namespace Application.EntityManagement.Answers.Commands;

public record CreateAnswerCommand(CreateAnswerDto CreateAnswerDto) : IRequest<CommandResult>;