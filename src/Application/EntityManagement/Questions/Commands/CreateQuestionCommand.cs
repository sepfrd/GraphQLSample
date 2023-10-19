using Application.Common.Commands;
using Application.EntityManagement.Questions.Dtos;

namespace Application.EntityManagement.Questions.Commands;

public record CreateQuestionCommand(QuestionDto QuestionDto) : BaseCreateCommand<QuestionDto>(QuestionDto);