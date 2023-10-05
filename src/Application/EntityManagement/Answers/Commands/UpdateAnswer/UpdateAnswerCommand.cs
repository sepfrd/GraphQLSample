using Application.Common.Commands;
using Application.EntityManagement.Answers.Dtos;

namespace Application.EntityManagement.Answers.Commands.UpdateAnswer;

public abstract record UpdateAnswerCommand(UpdateAnswerDto UpdateAnswerDto) : BaseUpdateCommand<AnswerDto>(UpdateAnswerDto);