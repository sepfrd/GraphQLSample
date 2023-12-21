using Application.Common;
using Application.EntityManagement.Answers.Dtos;
using MediatR;

namespace Application.EntityManagement.Answers.Commands;

public record CreateAnswerCommand(AnswerDto AnswerDto) : IRequest<CommandResult>;