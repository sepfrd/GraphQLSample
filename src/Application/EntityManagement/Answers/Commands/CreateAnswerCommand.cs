using Application.Common.Commands;
using Application.EntityManagement.Answers.Dtos;

namespace Application.EntityManagement.Answers.Commands;

public record CreateAnswerCommand(AnswerDto AnswerDto) : BaseCreateCommand<AnswerDto>(AnswerDto);