using Application.Common.Commands;
using Application.EntityManagement.Answers.Dtos;

namespace Application.EntityManagement.Answers.Commands;

public record UpdateAnswerCommand(int Id, AnswerDto AnswerDto) : BaseUpdateCommand<AnswerDto>(Id, AnswerDto);